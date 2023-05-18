using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class TurbineService : ITurbineService
    {
        private readonly ITurbineRepository _repository;
        private readonly ICloudFuncService _funcService;
        private readonly IMapper _mapper;

        public TurbineService(ITurbineRepository repository, ICloudFuncService funcService, IMapper mapper)
        {
            _repository = repository;
            _funcService = funcService;
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

        public void UpdateTurbine(Turbine turbineToUpdate)
        {
            _repository.UpdateTurbine(turbineToUpdate);
            _repository.SaveChanges();
        }

        public void DeleteTurbine(Turbine turbineToDelete)
        {
            _repository.DeleteTurbine(turbineToDelete);
            _repository.SaveChanges();
        }

        public async Task<string> GetFormattedDescriptionById(int id, string functionKey)
        {
            var turbine = _repository.GetTurbineById(id);

            if(turbine == null)
            {
                return "";
            }

            var turbineDto = _mapper.Map<TurbineReadDto>(turbine);

            var formattedDescription = await _funcService.GetFormattedTurbineDescription(turbineDto, functionKey);

            return formattedDescription;
        }
    }
}
