namespace CarRentalSystem.Application.Behaviours
{
    using Exceptions;

    using FluentValidation;
    using FluentValidation.Results;

    using MediatR;

    public class RequestValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => this.validators = validators;

        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

            List<ValidationFailure> errors = this.validators
                .Select(v => v.ValidateAsync(context))
                .SelectMany(r => r.Result.Errors)
                .Where(f => f != null)
                .ToList();

            if (errors.Count != 0)
            {
                throw new ModelValidationException(errors);
            }

            return next();
        }
    }
}
