using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;

namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface ICommandService
    {
        IEnumerable<Command> GetAllCommands();

        Command GetCommandById(int id);
    }
}
