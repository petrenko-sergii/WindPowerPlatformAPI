using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Settings;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class CloudFuncUrlBuilderService : ICloudFuncUrlBuilderService
    {
        public string CreateTurbineDescFormatterUrl(string functionKey)
        {
            string url = $"{CloudFunctionConstants.FunctionAppUrl}{CloudFunctionConstants.TurbineDescFormatter}" +
                $"?code={functionKey}&{CloudFunctionConstants.paramSerialNumber}={{0}}&{CloudFunctionConstants.paramPrice}={{1}}";

            return url ;
        }
    }
}
