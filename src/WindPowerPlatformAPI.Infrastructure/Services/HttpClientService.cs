using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();

            Configure(httpClient);

            return httpClient;
        }

        private void Configure(HttpClient httpClient)
        {
            var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

            if (defaultRequestHeaders.Accept == null ||
                   !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new
                  MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
    }
}
