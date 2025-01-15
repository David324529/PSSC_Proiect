using System;

namespace Data.Exceptions
{
    public class InvalidOrderStateException : Exception
    {
        public InvalidOrderStateException(string state)
            : base($"Starea comenzii '{state}' nu este valida pentru aceasta operatiune.")
        {
        }
    }
}