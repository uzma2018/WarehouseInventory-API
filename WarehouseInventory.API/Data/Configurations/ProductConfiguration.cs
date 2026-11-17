using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseInventory.API.Models;

namespace WarehouseInventory.API.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.HasIndex(u => u.SKU).IsUnique();
            modelBuilder.Property(u => u.SKU).IsRequired().HasMaxLength(50);

            modelBuilder.Property(u => u.Name).IsRequired().HasMaxLength(150);

            modelBuilder.Property(u => u.Description).HasMaxLength(1000);

            modelBuilder.Property(u => u.Price).IsRequired().HasPrecision(18, 2);

            modelBuilder.Property(u => u.StockQuantity).IsRequired();

            modelBuilder.Property(u => u.ReorderLevel).IsRequired();

            modelBuilder.Property(u => u.IsActive).IsRequired();

            modelBuilder.Property(u => u.CreatedAtUtc).IsRequired();

            modelBuilder.Property(u => u.CategoryId).IsRequired();


            modelBuilder.HasOne(u=>u.Category)
                .WithMany(u=>u.Products)
                .HasForeignKey(u=>u.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
