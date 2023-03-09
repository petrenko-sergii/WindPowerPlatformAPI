using System.Collections.Generic;
using WindPowerPlatformAPI.Infrastructure.Dtos;

namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface ICommandService
    {
        IEnumerable<CommandReadDto> GetAllCommands();

        CommandReadDto GetCommandById(int id);

        CommandReadDto CreateCommand(CommandCreateDto commandCreateDto);
    }
}
