using System;
using Api.Models;
using Example.Domain.Repositories;
using Data.Models;

namespace Domain.Operations
{
    public sealed class InvoiceOrderOperation : DomainOperation<OrderModel, object, OrderModel>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IShipmentRepository _shipmentRepository;  
        public InvoiceOrderOperation(IInvoiceRepository invoiceRepository, IShipmentRepository shipmentRepository)
        {
            _invoiceRepository = invoiceRepository;
            _shipmentRepository = shipmentRepository;  
        }

        public override OrderModel Transform(OrderModel order, object? state)
        {
            if (order.TotalPrice <= 0)
            {
                throw new InvalidOperationException("Nu se poate genera factura pentru o comandă cu pretul total 0.");
            }

            if (order.OrderId <= 0)
            {
                throw new InvalidOperationException("Comanda nu are un ID valid.");
            }

            // Creeare factura
            var invoice = new InvoiceDto
            {
                OrderId = order.OrderId,  // Folosim ID-ul corect al comenzii
                InvoiceNumber = $"INV-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                TotalAmount = order.TotalPrice
            };

            _invoiceRepository.AddInvoiceAsync(invoice).Wait();

            // Creare livrare (AWB)
            var shipment = new ShipmentDto
            {
                OrderId = order.OrderId,  // Folosim ID-ul corect al comenzii
                AWBNumber = $"AWB-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                Status = "Expediată"
            };

            _shipmentRepository.AddShipmentAsync(shipment).Wait();

            // Actualizare status comanda
            order.Status = "Facturată și Expediată"; // ??? where David

            return order; 
        }

    }
}