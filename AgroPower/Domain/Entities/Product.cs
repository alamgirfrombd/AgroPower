using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit.Sdk;

namespace AgroPower.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{5}$", ErrorMessage = "Code must be exactly 5 alphanumeric characters.")]
        public string Code { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //After Category 
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }  // Foreign Key from ProductCategory
        public ProductCategory Category { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= Quantity)
            {
                yield return new ValidationResult(
                    "মূল্য অবশ্যই পরিমাণের চেয়ে বেশি হতে হবে।",
                    new[] { nameof(Price), nameof(Quantity) }
                );
            }
        }
    }
}
