using Basket.API.Database.EntityTypeConfigurations;
using Basket.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Database
{
    public class BasketDbContext:DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options) {    }

        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CustomerBasket> CustomerBaskets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerBasketEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BasketItemEntityTypeConfiguration());
        }
    }
}
