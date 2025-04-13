using Moq;
using Xunit;
using System.Threading.Tasks;
using AgroPower.Application.Services;
using AgroPower.Domain.Entities;
using AgroPower.Domain.Interfaces;

public class ProductCategoryServiceTests
{
    [Fact]
    public async Task AddAsync_Should_Call_Repository_AddAsync()
    {
        // Arrange
        var mockRepo = new Mock<IProductCategoryRepository>();
        var productCategoryService = new ProductCategoryService(mockRepo.Object);

        var productCategory = new ProductCategory
        {
            Id = 1, // NewInt() মেথডটি কোথাও ডিফাইন করা হয়নি, তাই সরাসরি মান দেওয়া হলো
            Name = "Test Product Category",
   
        };

        // Act
        await productCategoryService.AddAsync(productCategory);

        // Assert
        mockRepo.Verify(repo => repo.AddAsync(It.Is<ProductCategory>(p => p == productCategory)), Times.Once);
    }
}
