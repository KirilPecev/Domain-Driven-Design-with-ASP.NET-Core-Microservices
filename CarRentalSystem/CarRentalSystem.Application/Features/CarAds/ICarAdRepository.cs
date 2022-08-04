﻿namespace CarRentalSystem.Application.Features.CarAds
{
    using Contracts;
    using Domain.Models.CarAds;
    using Queries.Search;

    public interface ICarAdRepository : IRepository<CarAd>
    {
        Task<IEnumerable<CarAdListingModel>> GetCarAdListings(
             string? manufacturer = default,
             CancellationToken cancellationToken = default);

        Task<int> Total(CancellationToken cancellationToken = default);

        Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken);

        Task<Manufacturer> GetManufacturer(string manufacturer, CancellationToken cancellationToken);
    }
}
