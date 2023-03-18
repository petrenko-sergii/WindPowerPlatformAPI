using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Dtos;

namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface ITurbineService
    {
        IEnumerable<TurbineReadDto> GetAllTurbines();

        TurbineReadDto GetTurbineById(int id);

        TurbineReadDto CreateTurbine(TurbineCreateDto turbineCreateDto);

        void UpdateTurbine(Turbine turbineToUpdate);
    }
}
