using Microsoft.EntityFrameworkCore;
using ElectroStore.Core.Entities;

namespace ElectroStore.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);
            
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Price);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name);
        }
    }
}
