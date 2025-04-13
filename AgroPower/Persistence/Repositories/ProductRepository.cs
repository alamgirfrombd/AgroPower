using AgroPower.Domain.Entities;
using AgroPower.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroPower.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AgroPowerDbContext _context;

        public ProductRepository(AgroPowerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid id) => await _context.Products.FindAsync(id);

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetByNameAsync(string name) => 
            await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
