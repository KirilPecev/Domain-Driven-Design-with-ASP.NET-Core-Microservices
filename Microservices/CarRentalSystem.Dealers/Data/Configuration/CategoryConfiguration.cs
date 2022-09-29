namespace CarRentalSystem.Dealers.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    using static DataConstants.Category;

    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            builder
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MaxNameLength);

            builder
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(MaxDescriptionLength);
        }
    }
}
