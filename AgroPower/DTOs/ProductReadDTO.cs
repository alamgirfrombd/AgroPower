using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace AgroPower.DTOs
{
    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public int? ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        // Category Info
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
