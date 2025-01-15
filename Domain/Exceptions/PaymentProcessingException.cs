using System;

namespace Data.Exceptions
{
    public class PaymentProcessingException : Exception
    {
        public PaymentProcessingException(string message)
            : base($"Eroare la procesarea platii: {message}")
        {
        }
    }
}