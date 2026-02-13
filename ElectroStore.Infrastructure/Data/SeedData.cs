using ElectroStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Products.Any())
                {
                    return;
                }

                var laptops = new Category { Name = "Laptops", Description = "High performance laptops" };
                var smartphones = new Category { Name = "Smartphones", Description = "Latest smartphones" };

                context.Categories.AddRange(laptops, smartphones);
                await context.SaveChangesAsync();

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Gaming Laptop X1",
                        Description = "Powerful gaming laptop with RTX 4080",
                        Price = 1999.99m,
                        Stock = 10,
                        ImageUrl = "/images/laptop1.jpg",
                        CategoryId = laptops.Id
                    },
                    new Product
                    {
                        Name = "Smartphone Pro Max",
                        Description = "Flagship phone with 200MP camera",
                        Price = 1299.99m,
                        Stock = 50,
                        ImageUrl = "/images/phone1.jpg",
                        CategoryId = smartphones.Id
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
