namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface ICloudFuncUrlBuilderService
    {
        string CreateTurbineDescFormatterUrl(string functionKey);
    }
}
