using System;
using Api.Models;
using Domain.Operations;

namespace Domain.Workflows
{
    public class FacturareWorkflow
    {
        private readonly InvoiceOrderOperation _invoiceOrderOperation;

        public FacturareWorkflow(InvoiceOrderOperation invoiceOrderOperation)
        {
            _invoiceOrderOperation = invoiceOrderOperation;
        }

        public IOrderEvent Execute(OrderModel order)
        {
            try
            {
                order = _invoiceOrderOperation.Transform(order, null);

                return new OrderProcessedEvent(order); 
            }
            catch (Exception ex)
            {
                return new OrderProcessFailedEvent($"S-a produs o eroare pe parcursul facturarii: {ex.Message}");
            }
        }
    }
}