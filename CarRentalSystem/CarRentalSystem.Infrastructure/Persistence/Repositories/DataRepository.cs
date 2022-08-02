namespace CarRentalSystem.Infrastructure.Persistence.Repositories
{
    using System.Linq;

    using Application.Contracts;
    using Domain.Common;

    internal abstract class DataRepository<TEnitity> : IRepository<TEnitity>
        where TEnitity : class, IAggregateRoot
    {
        private readonly CarRentalDbContext db;

        protected DataRepository(CarRentalDbContext db) => this.db = db;

        protected IQueryable<TEnitity> All() => this.db.Set<TEnitity>();
    }
}
