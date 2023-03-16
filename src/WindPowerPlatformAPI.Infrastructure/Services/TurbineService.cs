using AutoMapper;
using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class TurbineService : ITurbineService
    {
        private readonly ITurbineRepository _repository;
        private readonly IMapper _mapper;

        public TurbineService(ITurbineRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<TurbineReadDto> GetAllTurbines()
        {
            var turbines = _repository.GetAllTurbines();

            return _mapper.Map<IEnumerable<TurbineReadDto>>(turbines);
        }

        public TurbineReadDto GetTurbineById(int id)
        {
            var turbine = _repository.GetTurbineById(id);

            return _mapper.Map<TurbineReadDto>(turbine);
        }

        public TurbineReadDto CreateTurbine(TurbineCreateDto turbineCreateDto)
        {
            var turbineModel = _mapper.Map<Turbine>(turbineCreateDto);
            _repository.CreateTurbine(turbineModel);
            _repository.SaveChanges();

            var createdTurbineId = _mapper.Map<TurbineReadDto>(turbineModel).Id;
            var createdTurbine = GetTurbineById(createdTurbineId);

            return createdTurbine;
        }
    }
}
