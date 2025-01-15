using Api.Models;

namespace Domain.Workflows
{
    public class OrderProcessedEvent : IOrderEvent
    {
        public OrderModel Order { get; }

        public OrderProcessedEvent(OrderModel order)
        {
            Order = order;
        }
    }
}