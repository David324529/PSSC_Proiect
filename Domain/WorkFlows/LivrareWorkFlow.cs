using Api.Models;
using Domain.Operations;

namespace Domain.Workflows
{
    public class LivrareWorkflow
    {
        private readonly ShipOrderOperation _shipOrderOperation;

        public LivrareWorkflow(ShipOrderOperation shipOrderOperation)
        {
            _shipOrderOperation = shipOrderOperation;
        }

       public IOrderEvent Execute(OrderModel order)
       {
           try
           {
               order = _shipOrderOperation.Transform(order, null);
       
               return new OrderProcessedEvent(order); 
           }
           catch (Exception ex)
           {
               return new OrderProcessFailedEvent($"S-a produs o eroare pe parcursul livrarii: {ex.Message}");
           }
       }

    }
}