using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;

namespace WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces
{
	public interface ITurbineRepository
	{
		bool SaveChanges();
		IEnumerable<Turbine> GetAllTurbines();
		Turbine GetTurbineById(int id);
		void CreateTurbine(Turbine turbine);
		void UpdateTurbine(Turbine turbine);
		void DeleteTurbine(Turbine turbine);
	}
}
