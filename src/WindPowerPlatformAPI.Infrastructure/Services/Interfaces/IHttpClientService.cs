using System.Net.Http;

namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface IHttpClientService
    {
        HttpClient GetHttpClient();
    }
}
