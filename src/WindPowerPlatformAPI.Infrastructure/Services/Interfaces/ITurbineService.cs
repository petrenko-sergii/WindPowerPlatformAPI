using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;

namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface ITurbineService
    {
        IEnumerable<Turbine> GetAllTurbines();

        Turbine GetTurbineById(int id);
    }
}
