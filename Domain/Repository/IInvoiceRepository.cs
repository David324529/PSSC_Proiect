using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Domain.Repositories
{
    public interface IInvoiceRepository
    {
        Task<List<InvoiceDto>> GetAllInvoicesAsync();
        Task<InvoiceDto> GetInvoiceByIdAsync(int invoiceId);
        Task AddInvoiceAsync(InvoiceDto invoice);
        Task UpdateInvoiceAsync(InvoiceDto invoice);
        Task DeleteInvoiceAsync(int invoiceId);
    }
}