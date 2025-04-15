using AgroPower.Application.Interfaces;
using AgroPower.Domain.Entities;
using AgroPower.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgroPower.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Product?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(Product product) => await _repository.AddAsync(product);
        public async Task UpdateAsync(Product product) => await _repository.UpdateAsync(product);
        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
        public async Task<Product?> GetByNameAsync(string name) => await _repository.GetByNameAsync(name);
        // AgroPower.Application/Services/ProductService.cs

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await _repository.GetAllWithCategoryAsync(); // ✅
        }



    }

}
