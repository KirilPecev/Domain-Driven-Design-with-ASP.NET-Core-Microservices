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
            => await this.data
                .Messages
                .FromSqlRaw($"SELECT * FROM Messages WHERE Type = '{0}' AND JSON_VALUE(serializedData, '$.{1}') = {2}",
                                    messageData.GetType().AssemblyQualifiedName, propertyFilter, identifier)
                .AnyAsync();
    }
}
