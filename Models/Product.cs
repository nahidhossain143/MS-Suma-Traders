using System.ComponentModel.DataAnnotations.Schema;

namespace RiceShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        // Category relation
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int Quantity { get; set; }
    }
}
