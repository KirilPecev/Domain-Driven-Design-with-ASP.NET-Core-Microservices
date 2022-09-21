namespace CarRentalSystem.Application.Identity.Commands.ChangePassword
{
    using Application.Common;
    using Application.Common.Contracts;

    using MediatR;

    public class ChangePasswordCommand : IRequest<Result>
    {
        public string CurrentPassword { get; set; } = default!;

        public string NewPassword { get; set; } = default!;

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
        {
            private readonly ICurrentUser currentUser;
            private readonly IIdentity identity;

            public ChangePasswordCommandHandler(
                ICurrentUser currentUser,
                IIdentity identity)
            {
                this.currentUser = currentUser;
                this.identity = identity;
            }

            public async Task<Result> Handle(
                ChangePasswordCommand request,
                CancellationToken cancellationToken)
                => await identity.ChangePassword(new ChangePasswordInputModel(
                    currentUser.UserId,
                    request.CurrentPassword,
                    request.NewPassword));
        }
    }
}
