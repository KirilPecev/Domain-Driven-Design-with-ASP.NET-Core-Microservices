namespace CarRentalSystem.Statistics.Messages
{
    using System.Threading.Tasks;

    using CarRentalSystem.Data.Models;
    using CarRentalSystem.Messages.Dealers;
    using CarRentalSystem.Services.Messages;

    using Data;
    using Data.Models;

    using MassTransit;

    using Microsoft.EntityFrameworkCore;

    public class CarAdCreatedConsumer : IConsumer<CarAdCreatedMessage>
    {
        private readonly StatisticsDbContext data;
        private readonly IMessageService messages;

        public CarAdCreatedConsumer(StatisticsDbContext data, IMessageService messages)
        {
            this.data = data;
            this.messages = messages;
        }

        public async Task Consume(ConsumeContext<CarAdCreatedMessage> context)
        {
            CarAdCreatedMessage message = context.Message;

            bool isDuplicated = await this.messages.IsDuplicated(
                message,
                nameof(CarAdCreatedMessage.CarAdId),
                message.CarAdId);

            if (isDuplicated)
            {
                return;
            }

            Statistics statistics = await this.data.Statistics.SingleOrDefaultAsync();

            statistics.TotalCarAds++;

            Message dataMessage = new Message(message);

            dataMessage.MarkAsPublished();

            this.data.Messages.Add(dataMessage);

            await this.data.SaveChangesAsync();
        }
    }
}
