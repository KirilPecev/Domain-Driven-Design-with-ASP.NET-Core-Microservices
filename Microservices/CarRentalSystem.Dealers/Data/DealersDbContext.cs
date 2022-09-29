using System.Collections.Generic;
using System.Reflection;

namespace CarRentalSystem.Dealers.Data
{
    public class DealersDbContext : MessageDbContext
    {
        public DealersDbContext(DbContextOptions<DealersDbContext> options)
            : base(options)
        {
        }

        public DbSet<CarAd> CarAds { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Dealer> Dealers { get; set; }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}
