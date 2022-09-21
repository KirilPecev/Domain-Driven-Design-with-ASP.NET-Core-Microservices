namespace CarRentalSystem.Infrastructure.Persistence.Configuration
{
    using Domain.Models.CarAds;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static CarRentalSystem.Domain.Dealerships.Models.ModelConstants.Manufacturer;

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
