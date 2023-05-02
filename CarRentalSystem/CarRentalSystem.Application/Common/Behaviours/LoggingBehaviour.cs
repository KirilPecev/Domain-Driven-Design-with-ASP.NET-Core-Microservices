namespace CarRentalSystem.Application.Common.Behaviours
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Microsoft.Extensions.Logging;

    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Request
            logger.LogInformation($"Handling {typeof(TRequest).Name}");

            IList<PropertyInfo> props = new List<PropertyInfo>(request.GetType().GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object? propValue = prop.GetValue(request, null);
                logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
            }

            var response = await next();

            //Response
            logger.LogInformation($"Handled {typeof(TResponse).Name}");

            return response;
        }
    }
}
