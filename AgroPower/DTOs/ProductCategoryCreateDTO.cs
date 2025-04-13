using System.ComponentModel.DataAnnotations;

namespace AgroPower.DTOs
{
    public class ProductCategoryCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
