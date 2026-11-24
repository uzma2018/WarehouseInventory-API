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
            modelBuilder.Property(u => u.SKU).HasMaxLength(50);

            modelBuilder.Property(u => u.Name).HasMaxLength(150);

            modelBuilder.Property(u => u.Price).HasPrecision(18, 2);


            modelBuilder.HasOne(u=>u.Category)
                .WithMany(u=>u.Products)
                .HasForeignKey(u=>u.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
