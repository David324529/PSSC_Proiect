using System.Linq;
using Api.Models;

namespace Domain.Operations
{
    internal sealed class OrderProcessingOperation : DomainOperation<OrderModel, object, OrderModel>
    {
        private readonly ValidateOrderOperation _validateOrderOperation;
        private readonly CalculateOrderOperation _calculateOrderOperation;
        private readonly PublishOrderOperation _publishOrderOperation;

        public OrderProcessingOperation(
            ValidateOrderOperation validateOrderOperation,
            CalculateOrderOperation calculateOrderOperation,
            PublishOrderOperation publishOrderOperation)
        {
            _validateOrderOperation = validateOrderOperation;
            _calculateOrderOperation = calculateOrderOperation;
            _publishOrderOperation = publishOrderOperation;
        }

        public override OrderModel Transform(OrderModel order, object? state)
        {
            // Validare comanda
            order = _validateOrderOperation.Transform(order, state);

            if (order.ValidationErrors.Any())
            {
                return order; // erori de validare =>comanda invalida
            }

            // Calculare pretul total
            order = _calculateOrderOperation.Transform(order, state);

            // Procesare comanda
            order = _publishOrderOperation.Transform(order, state);

            return order; 
        }
    }
}