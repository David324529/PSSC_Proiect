using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example.Domain.Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItemDto>> GetAllOrderItemsAsync();
        Task<OrderItemDto> GetOrderItemByIdAsync(int orderItemId);
        Task<List<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId);  
        Task AddOrderItemAsync(OrderItemDto orderItem);
        Task UpdateOrderItemAsync(OrderItemDto orderItem);
        Task DeleteOrderItemAsync(int orderItemId);
    }
}