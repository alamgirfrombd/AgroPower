using AgroPower.Application.Interfaces;
using AgroPower.Application.Services;
using AgroPower.Domain.Entities;
using AgroPower.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AgroPower.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_CorrectProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = new Product
            {
                Id = productId,
                Name = "Test Product",
                Code = "TP123",
                Quantity = 10,
                Price = 100,
                CategoryId = 1
            };

            _mockRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _service.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
        }

        [Fact]
        public async Task UpdateAsync_Should_Call_RepositoryUpdate()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Updated Product",
                Code = "UP123",
                Quantity = 20,
                Price = 150,
                CategoryId = 2
            };

            // Act
            await _service.UpdateAsync(product);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Call_RepositoryDelete()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act
            await _service.DeleteAsync(productId);

            // Assert
            _mockRepository.Verify(r => r.DeleteAsync(productId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_AllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product A", Code = "A001", Quantity = 5, Price = 50 },
                new Product { Id = Guid.NewGuid(), Name = "Product B", Code = "B001", Quantity = 10, Price = 100 }
            };

            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
