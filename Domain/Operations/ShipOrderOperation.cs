using System;
using Api.Models;

namespace Domain.Operations
{
    public sealed class ShipOrderOperation : DomainOperation<OrderModel, object, OrderModel>
    {
        public override OrderModel Transform(OrderModel order, object? state)
        {
            if (string.IsNullOrEmpty(order.DeliveryAddress))
            {
                throw new InvalidOperationException("Adresa de livrare lipseste.");
            }
            else
            {
                //Do nothing
            }

            order.Status = "Livrata";
           
            return order;
        }
    }
}