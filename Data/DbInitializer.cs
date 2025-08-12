using Microsoft.AspNetCore.Identity;
using RiceShop.Models;

namespace RiceShop.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context,
                                                 UserManager<ApplicationUser> userManager,
                                                 RoleManager<IdentityRole> roleManager)
        {
            // Ensure DB exists
            context.Database.EnsureCreated();

            // Create roles
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Customer"))
                await roleManager.CreateAsync(new IdentityRole("Customer"));

            // Create admin user
            var adminEmail = "admin@riceshop.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var res = await userManager.CreateAsync(admin, "Admin@123"); // change password once deployed
                if (res.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Seed categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Basmati" },
                    new Category { Name = "Jasmine" },
                    new Category { Name = "Brown Rice" },
                    new Category { Name = "White Rice" }
                );
                await context.SaveChangesAsync();
            }

            // Seed products
            if (!context.Products.Any())
            {
                var basmati = context.Categories.First(c => c.Name == "Basmati");
                var jasmine = context.Categories.First(c => c.Name == "Jasmine");

                context.Products.AddRange(
                    new Product { Name = "Premium Basmati 5kg", Description = "Aromatic Basmati rice", Price = 550.00m, CategoryId = basmati.Id, ImageUrl = "/images/rice1.jpg", Quantity = 100 },
                    new Product { Name = "Jasmine 5kg", Description = "Fragrant Jasmine rice", Price = 480.00m, CategoryId = jasmine.Id, ImageUrl = "/images/rice2.jpg", Quantity = 120 }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
