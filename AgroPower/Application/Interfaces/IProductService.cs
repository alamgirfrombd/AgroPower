using AgroPower.Domain.Entities;

namespace AgroPower.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task<Product?> GetByNameAsync(string name);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
