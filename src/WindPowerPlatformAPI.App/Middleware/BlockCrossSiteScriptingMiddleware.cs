using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WindPowerPlatformAPI.App.Middleware
{
    public class BlockCrossSiteScriptingMiddleware : IMiddleware
    {
        private readonly ICorsPolicyProvider _corsPolicyProvider;

        public BlockCrossSiteScriptingMiddleware(ICorsPolicyProvider corsPolicyProvider)
        {
            _corsPolicyProvider = corsPolicyProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.Append(HeaderNames.Vary, HeaderNames.Origin);
            var originHeaderCSRF = context.Request.Headers[HeaderNames.Origin];

            if (originHeaderCSRF.Any())
            {
                var appBaseUrl = $"{context.Request.Scheme}://{context.Request.Host}";
                var corsPolicy = await _corsPolicyProvider.GetPolicyAsync(context, null);

                if (!originHeaderCSRF.All(x => string.Equals(x, appBaseUrl, StringComparison.OrdinalIgnoreCase)
                                           || corsPolicy.IsOriginAllowed(x)))
                {
                    Console.WriteLine($"Detected and blocked cross-site scripting from {originHeaderCSRF}");
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            await next(context);
        }
    }
}
