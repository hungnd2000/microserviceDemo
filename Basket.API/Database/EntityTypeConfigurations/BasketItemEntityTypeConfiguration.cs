using Basket.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.API.Database.EntityTypeConfigurations
{
    public class BasketItemEntityTypeConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasIndex(b => b.ProductId)
            .IsUnique();
            builder.ToTable("BasketItem");
            builder.HasKey(b => b.Id).HasName("PK_BasketItem");
            builder.Property(b => b.Id).HasColumnName("BasketItemId");
            builder.Property(b => b.ProductId).HasColumnName("ProductId");
            builder.Property(b => b.ProductName).HasColumnName("ProductName");
            builder.Property(b => b.Quantity).HasColumnName("Quantity");
            builder.Property(b => b.Status).HasColumnName("Status");
            builder.Property<int>("CustomerBasketId").IsRequired();
        }

    }
}
