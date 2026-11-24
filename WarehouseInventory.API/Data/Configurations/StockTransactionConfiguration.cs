using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseInventory.API.Models;

namespace WarehouseInventory.API.Data.Configurations
{
    public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> modelBuilder)
        {

            modelBuilder.Property(u => u.TransactionType).HasConversion<string>()
                .HasMaxLength(30);


            modelBuilder.HasOne(u=>u.Product)
                .WithMany(u=>u.StockTransactions)
                .HasForeignKey(u=>u.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasOne(u=>u.User)
                .WithMany(u=>u.StockTransactions)
                .HasForeignKey(u=>u.PerformedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
