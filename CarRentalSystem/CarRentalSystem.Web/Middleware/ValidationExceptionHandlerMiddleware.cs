namespace CarRentalSystem.Web.Middleware
{
    using System.Net;

    using Application.Exceptions;

    using CarRentalSystem.Application.Common.Exceptions;
    using CarRentalSystem.Domain.Common;
    using Domain.Exceptions;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class ValidationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ValidationExceptionHandlerMiddleware(RequestDelegate next)
            => this.next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleAsyncException(httpContext, ex);
            }
        }

        private static Task HandleAsyncException(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;

            string result = string.Empty;

            switch (exception)
            {
                case ModelValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = SerializeObject(new
                    {
                        ValidationDetails = true,
                        validationException.Errors
                    });
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                string message = exception.Message;

                if (exception is BaseDomainException baseDomainException)
                {
                    message = baseDomainException.Message;
                }

                result = SerializeObject(new[] { message });
            }

            return context.Response.WriteAsync(result);
        }

        private static string SerializeObject(object obj)
           => JsonConvert.SerializeObject(obj, new JsonSerializerSettings
           {
               ContractResolver = new DefaultContractResolver
               {
                   NamingStrategy = new CamelCaseNamingStrategy(true, true)
               }
           });
    }

    public static class ValidationExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ValidationExceptionHandlerMiddleware>();
    }
}
