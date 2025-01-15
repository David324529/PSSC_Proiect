using System;

namespace Data.Exceptions
{
    public class InvalidShippingAddressException : Exception
    {
        public InvalidShippingAddressException(string address)
            : base($"Adresa de livrare '{address}' nu este valida.")
        {
        }
    }
}