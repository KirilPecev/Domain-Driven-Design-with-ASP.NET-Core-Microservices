namespace CarRentalSystem.Infrastructure.Persistence.Configuration
{
    using Domain.Models.Auditable;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Newtonsoft.Json;

    internal class AuditEntryConfiguration : IEntityTypeConfiguration<AuditEntry>
    {
        public void Configure(EntityTypeBuilder<AuditEntry> builder)
        {
            builder
                .Property(ae => ae.Changes)
                .HasConversion(value => JsonConvert.SerializeObject(value),
                                serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedValue));
        }
    }
}
