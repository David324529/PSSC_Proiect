    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Example.Domain.Repositories;

    namespace Data.Repositories
    {
        public class OrderItemRepository : IOrderItemRepository
        {
            private readonly ShoppingContext _context;

            public OrderItemRepository(ShoppingContext context)
            {
                _context = context;
            }

            public async Task<List<OrderItemDto>> GetAllOrderItemsAsync()
            {
                return await _context.OrderItems.ToListAsync();
            }

            public async Task<OrderItemDto> GetOrderItemByIdAsync(int orderItemId)
            {
                return await _context.OrderItems.FindAsync(orderItemId);
            }

            public async Task<List<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId)
            {
                return await _context.OrderItems
                    .Where(item => item.OrderId == orderId)
                    .ToListAsync();
            }

            public async Task AddOrderItemAsync(OrderItemDto orderItem)
            {
                await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateOrderItemAsync(OrderItemDto orderItem)
            {
                _context.OrderItems.Update(orderItem);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteOrderItemAsync(int orderItemId)
            {
                var orderItem = await _context.OrderItems.FindAsync(orderItemId);
                if (orderItem != null)
                {
                    _context.OrderItems.Remove(orderItem);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }