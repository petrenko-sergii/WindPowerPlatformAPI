using System.Collections.Generic;
using System.Threading.Tasks;
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

        void DeleteTurbine(Turbine turbineToDelete);

        Task<string> GetFormattedDescriptionById(int id, string functionKey);
    }
}
