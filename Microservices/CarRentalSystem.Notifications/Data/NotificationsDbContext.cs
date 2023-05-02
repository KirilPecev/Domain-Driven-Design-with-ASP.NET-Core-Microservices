using System.Reflection;

using CarRentalSystem.Data;

using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Notifications.Data
{
    public class NotificationsDbContext : MessageDbContext
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options)
        {
        }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
    }
}
