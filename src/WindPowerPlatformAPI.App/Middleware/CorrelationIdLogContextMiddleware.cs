using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using System.Linq;
using System.Threading.Tasks;

namespace WindPowerPlatformAPI.App.Middleware
{
    public class CorrelationIdLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
            {
                return _next.Invoke(context);
            }
        }

        private string GetCorrelationId(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("Cko-Correlation-Id", out StringValues correlationId);

            return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }
    }
}
