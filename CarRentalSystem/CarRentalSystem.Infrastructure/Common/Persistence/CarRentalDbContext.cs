namespace CarRentalSystem.Infrastructure.Common.Persistence
{
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    using Auditable;

    using Common.Events;

    using Dealerships;

    using Domain.Common;
    using Domain.Common.Models;
    using Domain.Dealerships.Models.CarAds;
    using Domain.Dealerships.Models.Dealers;
    using Domain.Statistics.Models;

    using Identity;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Statistics;

    internal class CarRentalDbContext : IdentityDbContext<User, Role, string>,
        IDealershipDbContext,
        IStatisticsDbContext
    {
        private readonly string userId;
        private readonly IEventDispatcher eventDispatcher;

        public CarRentalDbContext(
            DbContextOptions<CarRentalDbContext> options,
            IHttpContextAccessor httpContextAccessor,
            IEventDispatcher eventDispatcher)
            : base(options)
        {
            ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;
            userId = user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Unauthenticated user";

            this.eventDispatcher = eventDispatcher;
        }

        public DbSet<CarAd> CarAds { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<Manufacturer> Manufacturers { get; set; } = default!;

        public DbSet<Dealer> Dealers { get; set; } = default!;

        public DbSet<AuditEntry> AuditEntries { get; set; } = default!;

        public DbSet<Statistic> Statistics { get; set; } = default!;

        public DbSet<CarAdView> CarAdViews { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            List<AuditEntry> auditEntries = this.OnBeforeSaveChanges();

            await this.DispatchEvents();

            int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            await this.OnAfterSaveChangesAsync(auditEntries);

            return result;
        }

        private async Task DispatchEvents()
        {
            IEntity[] entities = this.ChangeTracker
                            .Entries<IEntity>()
                            .Select(e => e.Entity)
                            .Where(e => e.Events.Any())
                            .ToArray();

            foreach (var entity in entities)
            {
                IDomainEvent[] events = entity.Events.ToArray();

                entity.ClearEvents();

                foreach (var domainEvent in events)
                {
                    await this.eventDispatcher.Dispatch(domainEvent);
                }
            }
        }

        private Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0) return Task.CompletedTask;

            // For each temporary property in each audit entry - update the value in the audit entry to the actual (generated) value
            foreach (var entry in auditEntries)
            {
                foreach (var prop in entry.TempProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.UpdateEntityId(prop.CurrentValue.ToString());
                    }

                    entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                }
            }

            this.AuditEntries.AddRange(auditEntries);

            return SaveChangesAsync();
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            List<AuditEntry> entries = new();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Dot not audit entities that are not tracked, not changed, or not of type IAuditable
                if (entry.State == EntityState.Detached
                    || entry.State == EntityState.Unchanged
                    || !(entry.Entity is IAuditable))
                    continue;

                AuditEntry auditEntry = new AuditEntry(entry, userId);

                entries.Add(auditEntry);
            }

            return entries;
        }
    }
}
