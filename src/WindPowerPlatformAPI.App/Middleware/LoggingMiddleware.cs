using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

namespace WindPowerPlatformAPI.App.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;

        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);

            await _next(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            var request = context.Request;

            if (request.Method == HttpMethod.Post.Method
                || request.Method == HttpMethod.Put.Method
                || request.Method == HttpMethod.Delete.Method)
            {

                request.EnableBuffering();
                var requestBody = await ReadBodyFromRequest(request);

                var _logger = _loggerFactory.CreateLogger<LoggingMiddleware>();

                if (request.Method == HttpMethod.Delete.Method)
                {
                    _logger.LogInformation($"Method \"{request.Path}\" of type: \"{request.Method}\" was called.");
                }
                else
                {
                    _logger.LogInformation($"Method \"{request.Path}\" of type: \"{request.Method}\" with the next body: {requestBody} was called.");
                }
            }
        }

        private async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            // Ensure the request's body can be read multiple times 
            // (for the next middlewares in the pipeline).
            request.EnableBuffering();

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();
            
            // Reset the request's body stream position for 
            // next middleware in the pipeline.
            request.Body.Position = 0;

            return requestBody;
        }
    }
}
