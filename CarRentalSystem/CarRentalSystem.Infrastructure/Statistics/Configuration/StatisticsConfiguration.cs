namespace CarRentalSystem.Infrastructure.Statistics.Configuration
{
    using Domain.Statistics.Models.Statistics;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StatisticsConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            const string id = "Id";

            builder
                .Property<int>(id);

            builder
                .HasKey(id);

            builder
                .HasMany(d => d.CarAdViews)
                .WithOne()
                .Metadata
                .PrincipalToDependent
                .SetField("carAdViews");
        }
    }
}
