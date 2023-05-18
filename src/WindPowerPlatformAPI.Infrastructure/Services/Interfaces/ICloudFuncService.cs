using System.Threading.Tasks;
using WindPowerPlatformAPI.Infrastructure.Dtos;

namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface ICloudFuncService
    {
        Task<string> GetFormattedTurbineDescription(TurbineReadDto turbine, string functionKey);
    }
}
