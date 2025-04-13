using AgroPower.Application.Interfaces;
using AgroPower.Domain.Entities;
using AgroPower.Domain.Interfaces;

namespace AgroPower.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryService(IProductCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<ProductCategory?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<ProductCategory?> GetByNameAsync(string name)
            => await _repository.GetByNameAsync(name);

        public async Task AddAsync(ProductCategory category)
            => await _repository.AddAsync(category);

        public async Task UpdateAsync(ProductCategory category)
            => await _repository.UpdateAsync(category);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }

}
