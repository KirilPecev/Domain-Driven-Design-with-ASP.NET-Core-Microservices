namespace CarRentalSystem.Infrastructure.Common.Persistence.Auditable
{
    using Domain.Common.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class AuditEntry : Entity<int>
    {
        private const string InserState = "INSERT";
        private const string DeleteState = "DELETE";
        private const string UpdateState = "UPDATE";

        public string EntityName { get; private set; }

        public string ActionType { get; private set; }

        public string UserId { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public string EntityId { get; private set; }

        public Dictionary<string, object> Changes { get; private set; }

        public List<PropertyEntry> TempProperties { get; private set; }

        internal AuditEntry(EntityEntry entry, string userId)
        {
            EntityName = entry.Metadata.ClrType.Name;
            ActionType = entry.State == EntityState.Added ? InserState : entry.State == EntityState.Deleted ? DeleteState : UpdateState;
            UserId = userId;
            TimeStamp = DateTime.UtcNow;
            EntityId = entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString();
            Changes = entry.Properties.Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue);

            // TempProperties are properties that are only generated on save, e.g. ID's
            // These properties will be set correctly after the audited entity has been saved
            TempProperties = entry.Properties.Where(p => p.IsTemporary).ToList();
        }

        private AuditEntry(
            string entityName,
            string actionType,
            string userId,
            DateTime timeStamp,
            string entityId)
        {
            EntityName = entityName;
            ActionType = actionType;
            UserId = userId;
            TimeStamp = timeStamp;
            EntityId = entityId;
            Changes = new();
            TempProperties = new();
        }

        public void UpdateEntityId(string entityId)
        {
            EntityId = entityId;
        }
    }
}
