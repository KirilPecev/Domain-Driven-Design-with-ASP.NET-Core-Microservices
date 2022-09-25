namespace CarRentalSystem.Infrastructure.Common.Persistence
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain.Common;

    internal abstract class DataRepository<TDbContext, TEntity> : IDomainRepository<TEntity>
        where TDbContext : IDbContext
        where TEntity : class, IAggregateRoot
    {
        protected CarRentalDbContext Data { get; }

        protected DataRepository(CarRentalDbContext db) => Data = db;

        protected IQueryable<TEntity> All() => Data.Set<TEntity>();

        public async Task Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);

            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
