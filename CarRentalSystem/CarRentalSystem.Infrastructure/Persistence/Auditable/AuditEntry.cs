namespace CarRentalSystem.Infrastructure.Persistence.Auditable
{
    using CarRentalSystem.Domain.Common.Models;
    using Domain.Common;

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
            this.EntityName = entry.Metadata.ClrType.Name;
            this.ActionType = entry.State == EntityState.Added ? InserState : entry.State == EntityState.Deleted ? DeleteState : UpdateState;
            this.UserId = userId;
            this.TimeStamp = DateTime.UtcNow;
            this.EntityId = entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString();
            this.Changes = entry.Properties.Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue);

            // TempProperties are properties that are only generated on save, e.g. ID's
            // These properties will be set correctly after the audited entity has been saved
            this.TempProperties = entry.Properties.Where(p => p.IsTemporary).ToList();
        }

        private AuditEntry(
            string entityName,
            string actionType,
            string userId,
            DateTime timeStamp,
            string entityId)
        {
            this.EntityName = entityName;
            this.ActionType = actionType;
            this.UserId = userId;
            this.TimeStamp = timeStamp;
            this.EntityId = entityId;
            this.Changes = new();
            this.TempProperties = new();
        }

        public void UpdateEntityId(string entityId)
        {
            this.EntityId = entityId;
        }
    }
}
