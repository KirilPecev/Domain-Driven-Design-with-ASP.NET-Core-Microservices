namespace CarRentalSystem.Application.Features.Identity.Commands.LoginUser
{
    using Application.Common;
    using Features.Dealers;
    using Features.Identity;

    using MediatR;

    public class LoginUserCommand : UserInputModel, IRequest<Result<LoginOutputModel>>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginOutputModel>>
        {
            private readonly IIdentity identity;
            private readonly IDealerRepository dealerRepository;

            public LoginUserCommandHandler(
                IIdentity identity,
                IDealerRepository dealerRepository)
            {
                this.identity = identity;
                this.dealerRepository = dealerRepository;
            }

            public async Task<Result<LoginOutputModel>> Handle(
                LoginUserCommand request,
                CancellationToken cancellationToken)
            {
                Result<LoginSuccessModel> result = await this.identity.Login(request);

                if (!result.Succeeded)
                {
                    return result.Errors;
                }

                LoginSuccessModel user = result.Data;

                int dealerId = await this.dealerRepository.GetDealerId(user.UserId, cancellationToken);

                return new LoginOutputModel(user.Token, dealerId);
            }
        }
    }
}
