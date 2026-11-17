using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseInventory.API.Models;

namespace WarehouseInventory.API.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
       public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder.HasIndex(u => u.Name).IsUnique();
            modelBuilder.Property(u => u.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Property(u => u.Description).HasMaxLength(500);
            
        }
    }
}
