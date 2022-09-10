namespace CarRentalSystem.Domain.Models.Auditable
{
    using Common;

    public class AuditEntry : Entity<int>
    {
        public string EntityName { get; private set; }

        public string ActionType { get; private set; }

        public string Username { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public string EntityId { get; private set; }

        public Dictionary<string, object> Changes { get; private set; }

        internal AuditEntry(
            string entityName,
            string actionType,
            string username,
            DateTime timeStamp,
            string entityId,
            Dictionary<string, object> changes)
        {
            this.EntityName = entityName;
            this.ActionType = actionType;
            this.Username = username;
            this.TimeStamp = timeStamp;
            this.EntityId = entityId;
            this.Changes = changes;
        }

        private AuditEntry(
            string entityName,
            string actionType,
            string username,
            DateTime timeStamp,
            string entityId)
        {
            this.EntityName = entityName;
            this.ActionType = actionType;
            this.Username = username;
            this.TimeStamp = timeStamp;
            this.EntityId = entityId;
            this.Changes = new();
        }
    }
}
