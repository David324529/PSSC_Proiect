using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProductsRepository
    {
        private readonly ShoppingContext _context;

        public ProductsRepository(ShoppingContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }
    }
}