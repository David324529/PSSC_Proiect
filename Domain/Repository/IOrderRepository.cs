using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(OrderDto order);
        Task UpdateOrderAsync(OrderDto order);
        Task DeleteOrderAsync(int orderId);
    }
}