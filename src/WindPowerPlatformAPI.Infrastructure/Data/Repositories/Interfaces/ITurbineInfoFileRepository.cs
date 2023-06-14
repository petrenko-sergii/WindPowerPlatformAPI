using WindPowerPlatformAPI.Domain.Entities;

namespace WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces
{
	public interface ITurbineInfoFileRepository
	{
		bool SaveChanges();
		TurbineInfoFile GetInformationFile(int turbineId);
		TurbineInfoFile GetInformationFileByName(string name);
		void CreateTurbineInfoFile(TurbineInfoFile infoFile);
	}
}
