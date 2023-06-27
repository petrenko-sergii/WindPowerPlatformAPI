using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using WindPowerPlatformAPI.Infrastructure.Exceptions;

namespace WindPowerPlatformAPI.App.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var responseCode = GetStatusCode(ex);
            var messageTemplate = "Error: {Exception} of type {ExceptionType} was thrown.";

            switch (responseCode)
            {
                case HttpStatusCode.NotFound:
                case HttpStatusCode.BadRequest:
                    _logger.LogWarning(messageTemplate, ex, ex.GetType());
                    break;
                default:
                    _logger.LogError(messageTemplate, ex, ex.GetType());
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)responseCode;

            var responseText = JsonConvert.SerializeObject(new { message = ex.Message, traceId = context.TraceIdentifier, stackTrace = ex.ToString() });

            return context.Response.WriteAsync(responseText);
        }

        private HttpStatusCode GetStatusCode(Exception ex)
        {
            switch (ex)
            {
                case NotFoundException:
                    return HttpStatusCode.NotFound;

                case BadArgumentException:
                    return HttpStatusCode.BadRequest;

                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
