namespace CarRentalSystem.Application.Features.CarAds.Commands.Common
{
    using Application.Common;
    using Contracts;
    using Features.Dealers;

    internal static class ChangeCarAdCommandExtensions
    {
        public static async Task<Result> HasDealerACarAd(
           this ICurrentUser currentUser,
           IDealerRepository dealerRepository,
           int carAdId,
           CancellationToken cancellationToken)
        {
            int dealerId = await dealerRepository.GetDealerId(currentUser.UserId, cancellationToken);

            bool dealerHasCar = await dealerRepository.HasCarAd(dealerId, carAdId, cancellationToken);

            return dealerHasCar ? Result.Success : "You cannot edit this car ad.";
        }
    }
}
