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

            modelBuilder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Electronic devices and accessories"
                },
                new Category
                {
                    Id = 2,
                    Name = "Office Supplies",
                    Description = "Office stationery and supplies"
                },
                new Category
                {
                    Id = 3,
                    Name = "Raw Materials",
                    Description = "Materials used in production"
                },
                new Category
                {
                    Id = 4,
                    Name = "Packaging",
                    Description = "Packaging materials and supplies"
                },
                new Category
                {
                    Id = 5,
                    Name = "Tools and Equipment",
                    Description = "Tools and equipment used for operations"
                }
            );

        }
    }
}
