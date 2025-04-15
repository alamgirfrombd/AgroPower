using System.ComponentModel.DataAnnotations;

namespace AgroPower.DTOs
{
    
    public class ProductCreateDto : IValidatableObject
    {
        //It is working properly of validation
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        //[Range(1, 99999, ErrorMessage = "ProductId must be a 5-digit number (00001-99999).")]
        public int? ProductId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]{5}$", ErrorMessage = "কোড অবশ্যই ৫ সংখ্যার হতে হবে, যেমন:০০০০১")]
        public string Code { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }

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
