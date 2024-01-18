using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.API.Entities;

namespace Order.API.Database.EntityTypeConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.ToTable("Order");
            builder
                .HasKey(e => e.Id)
                .HasName("Tbl_OrderId_PK");
            builder.Property(e => e.Id).HasColumnName("OrderId");
            builder.Property(e => e.OrderDate).HasColumnName("OrderDate");
            builder.Property(e => e.Street).HasColumnName("Street");
            builder.Property(e => e.District).HasColumnName("District");
            builder.Property(e => e.City).HasColumnName("City");
            builder.Property(e => e.AdditionalAddress).HasColumnName("AdditionalAddress");
            builder.Property(e => e.CustomerId).HasColumnName("CustomerId");
            builder.HasOne<Customer>()
            .WithMany()
            .IsRequired(false)
            .HasForeignKey(e => e.CustomerId);

            var navigation = builder.Metadata.FindNavigation(nameof(OrderModel.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
