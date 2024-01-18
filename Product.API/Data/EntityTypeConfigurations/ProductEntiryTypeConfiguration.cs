using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.API.Entities;

namespace Product.API.Data.EntityTypeConfigurations
{
    public class ProductEntiryTypeConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.ToTable(nameof(Product));
            builder.HasKey(x => x.Id).HasName("PK_Product");
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.Price).HasColumnName("Price");
            builder.Property(x => x.AvailableQuantity).HasColumnName("AvailableQuantity");
        }
    }
}
