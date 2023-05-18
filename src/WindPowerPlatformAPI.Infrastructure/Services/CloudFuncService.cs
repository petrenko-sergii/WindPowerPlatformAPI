using System.Net.Http;
using System.Threading.Tasks;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class CloudFuncService : ICloudFuncService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ICloudFuncUrlBuilderService _funcUrlBuilderService;

        public CloudFuncService(IHttpClientService httpClientService, ICloudFuncUrlBuilderService funcUrlBuilderService)
        {
            _httpClientService = httpClientService;
            _funcUrlBuilderService = funcUrlBuilderService;
        }

        public async Task<string> GetFormattedTurbineDescription(TurbineReadDto turbine, string functionKey)
        {
            var httpClient = _httpClientService.GetHttpClient();
            var functionUrl = _funcUrlBuilderService.CreateTurbineDescFormatterUrl(functionKey);
            var functionUrlWithParams = string.Format(functionUrl, turbine.SerialNumber, turbine.Price);

            HttpResponseMessage response = await httpClient.GetAsync(functionUrlWithParams);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                if (result.Contains("\\"))
                {
                    result = result.Replace("\\", "");
                }

                return result;
            }

            return "Error happened during Azure function call.";
        }
    }
}
