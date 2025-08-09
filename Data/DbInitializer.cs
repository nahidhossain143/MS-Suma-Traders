using Microsoft.AspNetCore.Identity;
using RiceShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiceShop.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // Create Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Customer"))
                await roleManager.CreateAsync(new IdentityRole("Customer"));

            // Create Admin User
            var adminEmail = "admin@rice.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Categories
            if (context.Categories.Any())
                return; // DB has been seeded

            var categories = new List<Category>
            {
                new Category { Name = "Basmati" },
                new Category { Name = "Jasmine" },
                new Category { Name = "Brown Rice" },
                new Category { Name = "White Rice" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Seed Products
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Bangladeshi Basmati Rice",
                    Description = "Premium quality basmati rice from Bangladesh",
                    Price = 120.00m,
                    CategoryId = categories.First(c => c.Name == "Basmati").Id,
                    ImageUrl = "/images/basmati1.jpg",
                    Quantity = 100
                },
                new Product
                {
                    Name = "Jasmine Rice Premium",
                    Description = "Fragrant Jasmine rice imported",
                    Price = 150.00m,
                    CategoryId = categories.First(c => c.Name == "Jasmine").Id,
                    ImageUrl = "/images/jasmine1.jpg",
                    Quantity = 200
                },
                new Product
                {
                    Name = "Brown Rice Organic",
                    Description = "Healthy organic brown rice",
                    Price = 130.00m,
                    CategoryId = categories.First(c => c.Name == "Brown Rice").Id,
                    ImageUrl = "/images/brown1.jpg",
                    Quantity = 150
                }
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
