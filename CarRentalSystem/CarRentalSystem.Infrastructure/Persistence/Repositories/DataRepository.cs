namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Contracts;
    using Domain.Common;

    internal class DataRepository<TEnitity> : IRepository<TEnitity>
        where TEnitity : class, IAggregateRoot
    {
        private readonly CarRentalDbContext db;

        public DataRepository(CarRentalDbContext db) => this.db = db;

        public IQueryable<TEnitity> All() => this.db.Set<TEnitity>();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => this.db.SaveChangesAsync(cancellationToken);
    }
}
