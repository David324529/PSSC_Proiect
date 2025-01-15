namespace Domain.Workflows
{
    public class OrderProcessFailedEvent : IOrderEvent
    {
        public List<string> Errors { get; }

        public OrderProcessFailedEvent(IEnumerable<string> errors)
        {
            Errors = errors.ToList();
        }

        public OrderProcessFailedEvent(string error)
        {
            Errors = new List<string> { error };
        }
    }
}