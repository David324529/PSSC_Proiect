using System;
using System.Threading.Tasks;
using Data.Exceptions;

namespace Domain.Operations
{
    public class PaymentService
    {
        public async Task<bool> ProcessPaymentAsync(decimal amount, string paymentMethod)
        {
            //procesare plata
            await Task.Delay(1000); 

            // Metoda plata
            if (string.IsNullOrEmpty(paymentMethod) || paymentMethod != "Card")
            {
                throw new PaymentProcessingException("Metoda de plata nu este acceptata.");
            }

            // Simulare: Dacă suma este mai mare de 10.000, simulam o eroare
            if (amount > 10000)
            {
                throw new PaymentProcessingException("Plata a fost respinsa pentru suma introdusa.");
            }

            return true; 
        }
    }
}