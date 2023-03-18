using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Dtos;

namespace WindPowerPlatformAPI.Infrastructure.Services.Interfaces
{
    public interface ICommandService
    {
        IEnumerable<CommandReadDto> GetAllCommands();

        CommandReadDto GetCommandById(int id);

        CommandReadDto CreateCommand(CommandCreateDto commandCreateDto);

        void UpdateCommand(Command commandToUpdate);

        void DeleteCommand(Command commandToDelete);
    }
}
