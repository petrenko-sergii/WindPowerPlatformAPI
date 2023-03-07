using System.Collections.Generic;
using WindPowerPlatformAPI.Infrastructure.Dtos;

namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface ITurbineService
    {
        IEnumerable<TurbineReadDto> GetAllTurbines();

        TurbineReadDto GetTurbineById(int id);
    }
}
