using System;
using System.Net;
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

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(functionUrlWithParams);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    if (result.Contains("\\"))
                    {
                        result = result.Replace("\\", "");
                    }

                    return result;
                } else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return HttpStatusCode.NotFound.ToString();
                }

                return HttpStatusCode.BadRequest.ToString();
            }
            catch(Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
