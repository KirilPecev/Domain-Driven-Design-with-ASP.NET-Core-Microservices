namespace CarRentalSystem.Application.Dealerships.Dealers.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Common;
    using Application.Common.Contracts;

    using Domain.Dealerships.Models.Dealers;
    using Domain.Dealerships.Repositories;

    using MediatR;

    public class EditDealerCommand : EntityCommand<int>, IRequest<Result>
    {
        public string Name { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public class EditDealerCommandHandler : IRequestHandler<EditDealerCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IDealerDomainRepository dealerRepository;

            public EditDealerCommandHandler(IDealerDomainRepository dealerRepository, ICurrentUser currentUser)
            {
                this.dealerRepository = dealerRepository;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(EditDealerCommand request, CancellationToken cancellationToken)
            {
                Dealer dealer = await dealerRepository.FindByUser(currentUser.UserId, cancellationToken);

                if (request.Id != dealer.Id) return "You cannot edit this dealer.";

                dealer
                    .UpdateName(request.Name)
                    .UpdatePhoneNumber(request.PhoneNumber);

                await dealerRepository.Save(dealer, cancellationToken);

                return Result.Success;
            }
        }
    }
}
