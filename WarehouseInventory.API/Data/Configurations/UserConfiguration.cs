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
            modelBuilder.Property(u => u.UserName).HasMaxLength(50);

            modelBuilder.Property(u => u.Name).HasMaxLength(100);

            modelBuilder.Property(u => u.PasswordHash).HasMaxLength(500);

            modelBuilder.Property(u => u.Role).HasMaxLength(50);

        }
    }
}
