using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class TurbineService : ITurbineService
    {
        private readonly ITurbineRepository _repository;

        public TurbineService(ITurbineRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Turbine> GetAllTurbines()
        {
            var turbines = _repository.GetAllTurbines();

            return turbines;
        }

        public Turbine GetTurbineById(int id)
        {
            var turbine = _repository.GetTurbineById(id);

            return turbine;
        }
    }
}
