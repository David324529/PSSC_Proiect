using System;

namespace Data.Exceptions
{
    public class OutOfStockException : Exception
    {
        public OutOfStockException(string productName)
            : base($"Produsul '{productName}' nu este pe stoc.")
        {
        }
    }
}