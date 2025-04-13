using AgroPower.Domain.Entities;
using AgroPower.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroPower.Persistence.Repositories
{
   
        public class ProductCategoryRepository : IProductCategoryRepository
        {
            private readonly AgroPowerDbContext _context;

            public ProductCategoryRepository(AgroPowerDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<ProductCategory>> GetAllAsync()
          => await _context.ProductCategory.ToListAsync();

            public async Task<ProductCategory?> GetByIdAsync(int id)
                => await _context.ProductCategory.FindAsync(id);

            public async Task<ProductCategory?> GetByNameAsync(string name)
                => await _context.ProductCategory
                                .FirstOrDefaultAsync(pc => pc.Name == name);

            public async Task AddAsync(ProductCategory category)
            {
                _context.ProductCategory.Add(category);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(ProductCategory category)
            {
                _context.ProductCategory.Update(category);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var category = await _context.ProductCategory.FindAsync(id);
                if (category != null)
                {
                    _context.ProductCategory.Remove(category);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }

