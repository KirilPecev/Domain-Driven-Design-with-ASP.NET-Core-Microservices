namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Contracts;
    using Domain.Common;

    internal abstract class DataRepository<TEnitity> : IRepository<TEnitity>
        where TEnitity : class, IAggregateRoot
    {
        protected CarRentalDbContext Data { get; }

        protected DataRepository(CarRentalDbContext db) => this.Data = db;

        protected IQueryable<TEnitity> All() => this.Data.Set<TEnitity>();

        public async Task Save(TEnitity entity, CancellationToken cancellationToken = default)
        {
            this.Data.Add(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
