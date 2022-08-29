﻿namespace CarRentalSystem.Application.Features.Dealers.Commands.Edit
{
    using System.Threading;
    using System.Threading.Tasks;
    using CarRentalSystem.Application.Common;
    using Contracts;
    using Domain.Models.Dealers;

    using MediatR;

    public class EditDealerCommand : EntityCommand<int>, IRequest<Result>
    {
        public string Name { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public class EditDealerCommandHandler : IRequestHandler<EditDealerCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IDealerRepository dealerRepository;

            public EditDealerCommandHandler(IDealerRepository dealerRepository, ICurrentUser currentUser)
            {
                this.dealerRepository = dealerRepository;
                this.currentUser = currentUser;
            }

            public async Task<Result> Handle(EditDealerCommand request, CancellationToken cancellationToken)
            {
                Dealer dealer = await this.dealerRepository.FindByUser(this.currentUser.UserId, cancellationToken);

                if (request.Id != dealer.Id) return "You cannot edit this dealer.";

                dealer
                    .UpdateName(request.Name)
                    .UpdatePhoneNumber(request.PhoneNumber);

                await this.dealerRepository.Save(dealer, cancellationToken);

                return Result.Success;
            }
        }
    }
}