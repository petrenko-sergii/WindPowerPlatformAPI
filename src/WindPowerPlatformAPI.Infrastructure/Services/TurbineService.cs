using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Constants;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;
using Turbine = WindPowerPlatformAPI.Domain.Entities.Turbine;

namespace WindPowerPlatformAPI.Infrastructure.Services
{
    public class TurbineService : ITurbineService
    {
        private readonly ITurbineRepository _repository;
        private readonly ITurbineInfoFileRepository _infoFileRepository;
        private readonly ICloudFuncService _funcService;
        private readonly IMapper _mapper;

        public TurbineService(
            ITurbineRepository repository,
            ITurbineInfoFileRepository infoFileRepository,
            ICloudFuncService funcService, 
            IMapper mapper)
        {
            _repository = repository;
            _infoFileRepository = infoFileRepository;
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

        public async Task<TurbineInfoFileReadDto> SaveTurbineInfoFile(IFormFile infoFile, int turbineId)
        {
            Validate(infoFile);
            ValidateForUniqueTurbineId(turbineId);

            using (var memoryStream = new MemoryStream())
            {
                await infoFile.CopyToAsync(memoryStream);

                var file = new TurbineInfoFile()
                {
                    Bytes = memoryStream.ToArray(),
                    Description = Path.GetFileNameWithoutExtension(infoFile.FileName),
                    FileExtension = Path.GetExtension(infoFile.FileName),
                    Size = infoFile.Length,
                    CreatedDt = DateTime.Now,
                    TurbineId = turbineId
                };

                try
                {
                    _infoFileRepository.CreateTurbineInfoFile(file);
                    _infoFileRepository.SaveChanges();

                    var createdInfoFile = _infoFileRepository.GetInformationFile(turbineId);
                    var fileReadDto = _mapper.Map<TurbineInfoFileReadDto>(createdInfoFile);

                    return fileReadDto;
                }
                catch (Exception ex)
                {

                    throw new Exception($"Error happened during file saving: {ex.Message}");
                }
            }
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

            if (turbine == null)
            {
                return "";
            }

            var turbineDto = _mapper.Map<TurbineReadDto>(turbine);

            var formattedDescription = await _funcService.GetFormattedTurbineDescription(turbineDto, functionKey);
            return formattedDescription;
        }

        private void Validate(IFormFile infoFile)
        {
            if (infoFile == null || infoFile.Length == 0)
            {
                throw new ArgumentException("Input file is null or empty");
            }

            if (infoFile.Length > InformationFileConstants.MaxSize)
            {
                throw new ArgumentException($"File {infoFile.FileName} exceeds size limit - {InformationFileConstants.MaxSize} bytes.");
            }

            var fileExtension = Path.GetExtension(infoFile.FileName);
            var allowedInfoFileExtensions = GetAllowedInfoFileExtensions();

            if (!allowedInfoFileExtensions.Contains(fileExtension))
            {
                throw new ArgumentException($"Wrong file extension. File must have one of the next extensions: " +
                    $"{String.Join(", ", allowedInfoFileExtensions.ToArray())}");
            }
        }

        private void ValidateForUniqueTurbineId(int turbineId)
        {
            var file = _infoFileRepository.GetInformationFile(turbineId);

            if (file != null)
            {
                throw new Exception($"File for this turbine  -- already exists.");
            }
        }


        private List<string> GetAllowedInfoFileExtensions()
        {
            return new List<string>{
                InformationFileConstants.PdfExtension,
                InformationFileConstants.JpgExtension,
                InformationFileConstants.JpegExtension
            }; ;
        }
    }
}
