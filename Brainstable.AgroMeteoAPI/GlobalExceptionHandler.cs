﻿using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.ErrorModel;

using Microsoft.AspNetCore.Diagnostics;

using System.Net;

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
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                logger.LogError($"Something went wrong: {contextFeature.Error}");

                await httpContext.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "Internal Server Error"
                }.ToString(), cancellationToken: cancellationToken);
            }

            return true;
        }
    }
}
