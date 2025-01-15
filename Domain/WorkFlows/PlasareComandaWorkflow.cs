using Api.Models;
using Data.Exceptions;
using Domain.Operations;

namespace Domain.Workflows
{
    public class PlasareComandaWorkflow
    {
        private readonly ValidateOrderOperation _validateOrderOperation;
        private readonly CalculateOrderOperation _calculateOrderOperation;
        private readonly PublishOrderOperation _publishOrderOperation;
        private readonly InvoiceOrderOperation _invoiceOrderOperation;
        private readonly ShipOrderOperation _shipOrderOperation;
        private readonly PaymentService _paymentService;

        public PlasareComandaWorkflow(
            ValidateOrderOperation validateOrderOperation,
            CalculateOrderOperation calculateOrderOperation,
            PublishOrderOperation publishOrderOperation,
            InvoiceOrderOperation invoiceOrderOperation,
            ShipOrderOperation shipOrderOperation,
            PaymentService paymentService)
        {
            _validateOrderOperation = validateOrderOperation;
            _calculateOrderOperation = calculateOrderOperation;
            _publishOrderOperation = publishOrderOperation;
            _invoiceOrderOperation = invoiceOrderOperation;
            _shipOrderOperation = shipOrderOperation;
            _paymentService = paymentService;
        }

        public IOrderEvent Execute(ProcessOrderCommand command)
        {
            try
            {
                // Validare comanda
                var order = new OrderModel
                {
                    CustomerId = command.CustomerId,
                    DeliveryAddress = command.DeliveryAddress,
                    OrderItems = command.OrderItems
                };

                order = _validateOrderOperation.Transform(order, null);
                if (order.ValidationErrors.Any())
                {
                    return new OrderProcessFailedEvent(order.ValidationErrors);
                }

                // Calculare preț total
                order = _calculateOrderOperation.Transform(order, null);

                // Procesare plata
                var paymentResult = _paymentService.ProcessPaymentAsync(order.TotalPrice, "Card").Result;
                if (!paymentResult)
                {
                    throw new PaymentProcessingException("Plata nu a fost efectuata.");
                }

                // Publicare comanda
                order = _publishOrderOperation.Transform(order, null);

                // Facturare
                order = _invoiceOrderOperation.Transform(order, null);

                // Livrare
                order = _shipOrderOperation.Transform(order, null);

                return new OrderProcessedEvent(order);
            }
            catch (PaymentProcessingException ex)
            {
                return new OrderProcessFailedEvent(new List<string> { $"Eroare la plata: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return new OrderProcessFailedEvent(new List<string> { $"Eroare generala: {ex.Message}" });
            }
        }
    }
}
