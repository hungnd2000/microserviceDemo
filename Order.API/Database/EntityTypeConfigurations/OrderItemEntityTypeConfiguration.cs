using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.API.Entities;

namespace Order.API.Database.EntityTypeConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.Property(e => e.Id).HasColumnName("OrderItemId");
            builder.Property(e => e.ProductId).HasColumnName("ProductId");
            builder.Property(e => e.ProductName).HasColumnName("ProductName");
            builder.Property(e => e.Quantity).HasColumnName("Quantity");
            builder.Property<int>("OrderId").IsRequired();
        }
    }
}
