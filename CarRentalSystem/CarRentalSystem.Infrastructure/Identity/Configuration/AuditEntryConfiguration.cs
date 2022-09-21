namespace CarRentalSystem.Infrastructure.Identity.Configuration
{
    using Common.Persistence.Auditable;

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

            builder
                .Ignore(ae => ae.TempProperties);
        }
    }
}
