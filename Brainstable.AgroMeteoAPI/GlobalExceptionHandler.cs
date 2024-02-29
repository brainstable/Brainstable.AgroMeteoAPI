using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.ErrorModel;

using Microsoft.AspNetCore.Diagnostics;

using System.Net;
using Brainstable.AgroMeteoAPI.Entities.Exceptions;

namespace Brainstable.AgroMeteoAPI
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILoggerManager logger;

        public GlobalExceptionHandler(ILoggerManager logger)
        {
            this.logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                httpContext.Response.StatusCode = contextFeature.Error switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                
                logger.LogError($"Something went wrong: {contextFeature.Error}");

                await httpContext.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = contextFeature.Error.Message
                }.ToString(), cancellationToken: cancellationToken);
            }

            return true;
        }
    }
}
