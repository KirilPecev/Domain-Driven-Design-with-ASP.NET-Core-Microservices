namespace CarRentalSystem.Infrastructure.Persistence
{
    using System.Reflection;

    using Domain.Models.Auditable;
    using Domain.Models.CarAds;
    using Domain.Models.Dealers;
    using Identity;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    internal class CarRentalDbContext : IdentityDbContext<User, Role, string>
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options) { }

        public DbSet<CarAd> CarAds { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Manufacturer> Manufacturers { get; set; } = default!;

        public DbSet<Dealer> Dealers { get; set; } = default!;

        public DbSet<AuditEntry> AuditEntries { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
