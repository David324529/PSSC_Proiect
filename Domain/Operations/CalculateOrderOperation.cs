using Api.Models;
using Example.Domain.Repositories;
using System.Linq;

namespace Domain.Operations
{
    public sealed class CalculateOrderOperation : DomainOperation<OrderModel, object, OrderModel>
    {
        private readonly IProductRepository _productRepository;

        public CalculateOrderOperation(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override OrderModel Transform(OrderModel order, object? state)
        {
            decimal totalPrice = 0;
            
            foreach (var item in order.OrderItems)
            {
                var product = _productRepository.GetProductByNameAsync(item.ProductName).Result;

                if (product != null)
                {
                    item.Price = product.Price;  // Pretul se ia din baza de date
                    totalPrice += item.Price * item.Quantity;
                }
            }
            order.TotalPrice = totalPrice;


            return order;
        }
    }
}