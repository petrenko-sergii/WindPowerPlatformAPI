using AutoMapper;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Dtos;

namespace WindPowerPlatformAPI.App.AutoMapper
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //source => destination
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandReadDto, Command>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}
