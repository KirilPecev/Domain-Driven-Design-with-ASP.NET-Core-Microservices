namespace CarRentalSystem.Dealers.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static CarRentalSystem.Data.DataConstants.Common;
    using static DataConstants.CarAd;

    internal class CarAdConfiguration : IEntityTypeConfiguration<CarAd>
    {
        public void Configure(EntityTypeBuilder<CarAd> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .UseHiLo(nameof(CarAd));

            builder
                .HasIndex(c => c.IsAvailable);

            builder
                .Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(MaxModelLength);

            builder
                .Property(c => c.ImageUrl)
                .IsRequired()
                .HasMaxLength(MaxUrlLength);

            builder
                .Property(c => c.PricePerDay)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .Property(c => c.IsAvailable)
                .IsRequired();

            builder
                .HasOne(c => c.Manufacturer)
                .WithMany()
                .HasForeignKey(c => c.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .OwnsOne(c => c.Options, o =>
                {
                    o.WithOwner();

                    o.Property(op => op.NumberOfSeats).IsRequired();

                    o.Property(op => op.HasClimateControl).IsRequired();

                    o.Property(op => op.TransmissionType).IsRequired();
                });
        }
    }
}
