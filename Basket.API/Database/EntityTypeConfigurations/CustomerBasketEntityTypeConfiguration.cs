using Basket.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.API.Database.EntityTypeConfigurations
{
    public class CustomerBasketEntityTypeConfiguration: IEntityTypeConfiguration<CustomerBasket>
    {
        public void Configure(EntityTypeBuilder<CustomerBasket> builder)
        {
            builder.ToTable("CustomerBasket");
            builder.HasKey(c => c.CustomerId).HasName("PK_CustomerBasket");
            builder.Property(c => c.CustomerId).HasColumnName("CustomerId");

            var navigation = builder.Metadata.FindNavigation(nameof(CustomerBasket.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
