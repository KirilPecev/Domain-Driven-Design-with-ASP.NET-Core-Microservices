namespace CarRentalSystem.Services.Messages
{
    using CarRentalSystem.Data;

    using Microsoft.EntityFrameworkCore;

    public class MessageService : IMessageService
    {
        private readonly MessageDbContext data;

        public MessageService(DbContext data)
            => this.data = data as MessageDbContext
                ?? throw new InvalidOperationException($"Messages can only be used with a {nameof(MessageDbContext)}.");

        public async Task<bool> IsDuplicated(object messageData, string propertyFilter, object identifier)
        {
            string prop = $"$.{propertyFilter}";

            return await this.data
                .Messages
                .FromSqlInterpolated($"SELECT * FROM Messages WHERE Type = {messageData.GetType().AssemblyQualifiedName} AND JSON_VALUE(serializedData, {prop}) = {identifier}")
                .AnyAsync();
        }
    }
}
