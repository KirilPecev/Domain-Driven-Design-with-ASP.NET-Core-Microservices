namespace CarRentalSystem.Web.Features
{
    using Application.Features.Identity.Commands.ChangePassword;
    using Application.Features.Identity.Commands.LoginUser;
    using Application.Features.Identity.Commands.RegisterUser;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserCommand command)
            => await this.Send(command);

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginUserCommand command)
            => await this.Send(command);

        [HttpPut]
        [Authorize]
        [Route(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
           => await this.Send(command);
    }
}
