using Microsoft.EntityFrameworkCore;
using Product.API.Data.EntityTypeConfigurations;
using Product.API.Entities;

namespace Product.API.Data
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options): base(options) { }

        public DbSet<ProductItem> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntiryTypeConfiguration());
        }
    }
}
