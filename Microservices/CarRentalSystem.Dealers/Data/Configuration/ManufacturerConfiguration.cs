namespace CarRentalSystem.Dealers.Data.Configuration
{
    using CarRentalSystem.Dealers.Data.Models;

    using Domain.Dealerships.Models.CarAds;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static CarRentalSystem.Dealers.Data.ModelConstants.Manufacturer;

    internal class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);
        }
    }
}
