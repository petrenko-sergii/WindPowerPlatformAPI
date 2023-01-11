using System;
using System.Collections.Generic;
using System.Text;
using WindPowerPlatformAPI.Domain.Entities;

namespace WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces
{
	public interface ICommandRepository
	{
		bool SaveChanges();
		IEnumerable<Command> GetAllCommands();
		Command GetCommandById(int id);
		void CreateCommand(Command cmd);
		void UpdateCommand(Command cmd);
		void DeleteCommand(Command cmd);
	}
}
