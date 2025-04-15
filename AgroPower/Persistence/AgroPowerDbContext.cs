using AgroPower.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AgroPower.DTOs;

namespace AgroPower.Persistence
{
    public class AgroPowerDbContext : DbContext
    {
        public AgroPowerDbContext(DbContextOptions<AgroPowerDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductCategory> ProductCategory { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any additional configuration here

            // For example, if you want to configure the Product entity
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
               .HasPrecision(18, 2);

            modelBuilder.Entity<ProductCategory>()
                .Property(pc => pc.Name)
                .IsRequired()
                .HasMaxLength(100);

        }
        public DbSet<AgroPower.DTOs.ProductUpdateDto> ProductUpdateDto { get; set; } = default!;
    }

}
