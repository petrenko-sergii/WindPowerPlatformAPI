using AutoMapper;
using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;

        public CommandService(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<CommandReadDto> GetAllCommands()
        {
            var commands = _repository.GetAllCommands();

            return _mapper.Map<IEnumerable<CommandReadDto>>(commands);
        }

        public CommandReadDto GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            return _mapper.Map<CommandReadDto>(commandItem);
        }

        public CommandReadDto CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var createdCommandId = _mapper.Map<CommandReadDto>(commandModel).Id;
            var createdCommand = GetCommandById(createdCommandId);

            return createdCommand;
        }
    }
}
