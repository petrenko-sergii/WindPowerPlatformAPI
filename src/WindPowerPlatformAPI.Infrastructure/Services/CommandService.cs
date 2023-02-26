using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _repository;

        public CommandService(ICommandRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = _repository.GetAllCommands();

            return commands;
        }

        public Command GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            return commandItem;
        }
    }
}
