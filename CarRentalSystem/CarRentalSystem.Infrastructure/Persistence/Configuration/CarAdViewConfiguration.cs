namespace CarRentalSystem.Infrastructure.Persistence.Configuration
{
    using Domain.Models.CarAds;
    using Domain.Models.Statistics;
    using Infrastructure.Identity;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarAdViewConfiguration : IEntityTypeConfiguration<CarAdView>
    {
        public void Configure(EntityTypeBuilder<CarAdView> builder)
        {
            builder
                .HasKey(cav => cav.Id);

            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<CarAd>()
                .WithMany()
                .HasForeignKey(c => c.CarAdId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
