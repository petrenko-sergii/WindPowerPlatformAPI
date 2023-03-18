using AutoMapper;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Dtos;

namespace WindPowerPlatformAPI.App.AutoMapper
{
    public class TurbinesProfile : Profile
    {
        public TurbinesProfile()
        {
            //source => destination
            CreateMap<Turbine, TurbineReadDto>();
            CreateMap<TurbineReadDto, Turbine>();
            CreateMap<TurbineCreateDto, Turbine>();
            CreateMap<TurbineUpdateDto, Turbine>();
        }
    }
}
