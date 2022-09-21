namespace CarRentalSystem.Application.Identity.Commands.RegisterUser
{
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Common;

    using Domain.Factories.Dealers;
    using Domain.Models.Dealers;
    using Domain.Repositories;

    using MediatR;

    public class RegisterUserCommand : UserInputModel, IRequest<Result>
    {
        public string Name { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
        {
            private readonly IIdentity identity;
            private readonly IDealerFactory dealerFactory;
            private readonly IDealerDomainRepository dealerRepository;

            public RegisterUserCommandHandler(
                IIdentity identity,
                IDealerFactory dealerFactory,
                IDealerDomainRepository dealerRepository)
            {
                this.identity = identity;
                this.dealerFactory = dealerFactory;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                Result<IUser> result = await this.identity.Register(request);

                if (!result.Succeeded) return result;

                IUser user = result.Data;

                Dealer dealer = this.dealerFactory
                    .WithName(request.Name)
                    .WithPhoneNumber(request.PhoneNumber)
                    .Build();

                user.BecomeDealer(dealer);

                await this.dealerRepository.Save(dealer, cancellationToken);

                return result;
            }
        }
    }
}
