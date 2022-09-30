namespace CarRentalSystem.Services.Messages
{
    public interface IMessageService
    {
        Task<bool> IsDuplicated(object messageData, string propertyFilter, object identifier);
    }
}
