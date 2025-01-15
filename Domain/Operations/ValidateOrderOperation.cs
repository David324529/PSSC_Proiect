using Api.Models;
using Example.Domain.Repositories;

namespace Domain.Operations
{
    public sealed class ValidateOrderOperation : DomainOperation<OrderModel, object, OrderModel>
    {
        private readonly IProductRepository _productRepository;

        public ValidateOrderOperation(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public override OrderModel Transform(OrderModel order, object? state)
        {
            List<string> validationErrors = new();

            foreach (var item in order.OrderItems)
            {
                var product = _productRepository.GetProductByNameAsync(item.ProductName).Result;

                if (product == null)
                {
                    validationErrors.Add($"Produsul {item.ProductName} nu exista.");
                }
                else if (item.Quantity <= 0)
                {
                    validationErrors.Add($"Cantitatea pentru produsul {item.ProductName} trebuie să fie mai mare decat 0.");
                }
                else if (item.Quantity > product.StockQuantity)  // Verificare stoc
                {
                    validationErrors.Add($"Cantitatea solicitata pentru {item.ProductName} depaseste stocul disponibil ({product.StockQuantity}).");
                }
            }

            if (validationErrors.Any())
            {
                order.ValidationErrors = validationErrors;
                return order;
            }

            return order;
        }

    }
}