using System;
using Api.Models;
using Example.Domain.Repositories;
using Data.Models;
using System.Threading.Tasks;

namespace Domain.Operations
{
    public sealed class PublishOrderOperation : DomainOperation<OrderModel, object, OrderModel>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public PublishOrderOperation(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public override OrderModel Transform(OrderModel order, object? state)
        {
            if (order.TotalPrice <= 0)
            {
                throw new InvalidOperationException("Pretul total al comenzii trebuie să fie mai mare decat 0.");
            }

            order.Status = "Plasata";
            order.PublishedDate = DateTime.Now;

            var orderDto = new OrderDto
            {
                CustomerId = order.CustomerId,
                DeliveryAddress = order.DeliveryAddress,
                TotalPrice = order.TotalPrice,
                Status = order.Status
            };

            try
            {
                // Inserare comanda
                _orderRepository.AddOrderAsync(orderDto).Wait();

                // Actualizare OrderModel cu ID-ul generat
                order.OrderId = orderDto.OrderId;

                //Veridicarea si actulaizarea stocul pentru fiecare produs
                foreach (var item in order.OrderItems)
                {
                    var product = _productRepository.GetProductByNameAsync(item.ProductName).Result;

                    if (product != null)
                    {
                        // Verificare stoc disponibil
                        if (item.Quantity > product.StockQuantity)
                        {
                            throw new InvalidOperationException($"Stoc insuficient pentru produsul {item.ProductName}. Disponibil: {product.StockQuantity}");
                        }

                        // Actualizare stoc
                        product.StockQuantity -= item.Quantity;
                        _productRepository.UpdateProductAsync(product).Wait();

                        // Inserare in tabela OrderItems
                        var orderItemDto = new OrderItemDto
                        {
                            OrderId = orderDto.OrderId,
                            ProductId = product.ProductId,
                            Quantity = item.Quantity,
                            Price = product.Price
                        };

                        _orderItemRepository.AddOrderItemAsync(orderItemDto).Wait();
                    }
                    else
                    {
                        throw new InvalidOperationException($"Produsul {item.ProductName} nu a fost gasit.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Daca apare o eroare, stergem comanda inserata partial
                _orderRepository.DeleteOrderAsync(orderDto.OrderId).Wait();
                throw new InvalidOperationException($"Eroare la plasarea comenzii: {ex.Message}");
            }

            return order;
        }
    }
}
