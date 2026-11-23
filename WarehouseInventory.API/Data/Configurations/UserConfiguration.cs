using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseInventory.API.Models;

namespace WarehouseInventory.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Property(u => u.UserName).IsRequired().HasMaxLength(50);

            modelBuilder.Property(u => u.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);

            modelBuilder.Property(u => u.Role).IsRequired().HasMaxLength(50);

            modelBuilder.Property(u => u.CreatedAtUtc).IsRequired();

        }
    }
}
