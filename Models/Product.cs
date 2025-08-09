using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiceShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Price in BDT

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
