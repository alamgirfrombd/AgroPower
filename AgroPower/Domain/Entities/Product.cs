using System.ComponentModel.DataAnnotations.Schema;

namespace AgroPower.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //After Category 
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }  // Foreign Key from ProductCategory
        public ProductCategory Category { get; set; }
    }
}
