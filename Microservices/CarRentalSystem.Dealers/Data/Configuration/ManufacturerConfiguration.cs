namespace CarRentalSystem.Dealers.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static DataConstants.Manufacturer;

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
