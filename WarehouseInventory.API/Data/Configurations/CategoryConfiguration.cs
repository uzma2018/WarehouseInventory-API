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
            modelBuilder.Property(u => u.Name).HasMaxLength(100);

            modelBuilder.Property(u => u.Description).HasMaxLength(500);

            modelBuilder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Electronic devices and accessories",
                    CreatedAtUtc = new DateTime(2026, 09, 24, 0, 0, 0, DateTimeKind.Utc),
                },
                new Category
                {
                    Id = 2,
                    Name = "Office Supplies",
                    Description = "Office stationery and supplies",
                    CreatedAtUtc = new DateTime(2026, 09, 25, 1, 10, 5, DateTimeKind.Utc),
                },
                new Category
                {
                    Id = 3,
                    Name = "Raw Materials",
                    Description = "Materials used in production",
                    CreatedAtUtc = new DateTime(2026, 10, 2, 2, 13, 25, DateTimeKind.Utc),
                },
                new Category
                {
                    Id = 4,
                    Name = "Packaging",
                    Description = "Packaging materials and supplies",
                    CreatedAtUtc = new DateTime(2026, 10, 10, 1, 15, 2, DateTimeKind.Utc),
                },
                new Category
                {
                    Id = 5,
                    Name = "Tools and Equipment",
                    Description = "Tools and equipment used for operations",
                    CreatedAtUtc = new DateTime(2026, 10, 20, 1, 20, 5, DateTimeKind.Utc),
                }
            );

        }
    }
}
