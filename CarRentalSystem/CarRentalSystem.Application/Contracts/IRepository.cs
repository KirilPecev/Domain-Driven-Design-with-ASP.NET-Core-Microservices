namespace CarRentalSystem.Application.Contracts
{
    using Domain.Common;

    public interface IRepository<in TEntity>
        where TEntity : IAggregateRoot
    {
        Task Save(TEntity entity, CancellationToken cancellationToken = default);
    }
}
