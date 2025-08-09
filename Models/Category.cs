using RiceShop.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiceShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}
