using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using MimeTypes;

namespace WindPowerPlatformAPI.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TurbinesController : ControllerBase
	{
		private readonly ITurbineService _service;
		private readonly IAzureResponseHelper _azureResponseHelper;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public TurbinesController(
			ITurbineService service,
            IAzureResponseHelper azureResponseHelper,
            IMapper mapper, 
			IConfiguration configuration)
        {
			_service = service;
			_azureResponseHelper = azureResponseHelper;
            _mapper = mapper;
			_configuration = configuration;
		}

		[HttpGet]
		public ActionResult<IEnumerable<TurbineReadDto>> GetAllTurbines()
		{
			var turbines = _service.GetAllTurbines();

			return Ok(turbines);
		}

		[Authorize]
		[HttpGet("{id}", Name = "GetTurbineById")]
		public ActionResult<TurbineReadDto> GetTurbineById(int id)
		{
			var turbine = _service.GetTurbineById(id);

			if (turbine == null)
				return NotFound();

			return Ok(turbine);
		}

        [HttpGet("{turbineId}/download-info-file", Name = "DownloadInformationFile")]
        public IActionResult DownloadInformationFile(int turbineId)
        {
			var fileDto = _service.GetTurbineInfoFile(turbineId);

			if(fileDto == null)
                return NotFound($"Information file for Turbine with Id = {turbineId} -- not found.");

            var contentType = MimeTypeMap.GetMimeType(fileDto.FileExtension);

            return File(fileDto.Bytes, contentType, $"{fileDto.Description}{fileDto.FileExtension}");
        }

        [HttpPost("{turbineId}/upload-info-file", Name = "UploadInformationFile")]
        public async Task<IActionResult> UploadInformationFile([FromForm] IFormFile infoFile, int turbineId)
        {
            var createdFile = await _service.SaveTurbineInfoFile(infoFile, turbineId);

            return CreatedAtRoute(nameof(DownloadInformationFile), new { turbineId = createdFile.TurbineId }, createdFile);
        }

        [Authorize]
        [HttpGet("{id}/format-description", Name = "GetFormattedDescriptionById")]
        public async Task<ActionResult<string>> GetFormattedDescriptionById(int id)
        {
            var funcKey = _configuration["FunctionApp:TurbineDescFormatterFunc:Key"];
           
            var formattedDescription = await _service.GetFormattedDescriptionById(id, funcKey);

            return _azureResponseHelper.CheckFormattedDescFuncResponse(formattedDescription);
        }

        [HttpPost]
		public ActionResult<TurbineReadDto> CreateTurbine(TurbineCreateDto turbineCreateDto)
		{
			if (turbineCreateDto == null)
			{
				throw new ArgumentNullException(nameof(turbineCreateDto));
			}

			var createdTurbine = _service.CreateTurbine(turbineCreateDto);

			return CreatedAtRoute(nameof(GetTurbineById), new { Id = createdTurbine.Id }, createdTurbine);
		}

		[HttpPut("{id}")]
		public ActionResult UpdateTurbine(int id, TurbineUpdateDto turbineUpdateDto)
		{
			var turbineModelFromRepo = _service.GetTurbineById(id);
			if (turbineModelFromRepo == null)
			{
				return NotFound();
			}

			var turbineEntity = _mapper.Map<Turbine>(turbineModelFromRepo);
			_mapper.Map(turbineUpdateDto, turbineEntity);

			_service.UpdateTurbine(turbineEntity);

			return NoContent();
		}

		[HttpPatch("{id}")]
		public ActionResult PartialTurbineUpdate(int id, JsonPatchDocument<TurbineUpdateDto> patchDoc)
		{
			var turbineModelFromRepo = _service.GetTurbineById(id);
			if (turbineModelFromRepo == null)
			{
				return NotFound();
			}

			var turbineEntity = _mapper.Map<Turbine>(turbineModelFromRepo);

			var turbineToPatch = _mapper.Map<TurbineUpdateDto>(turbineEntity);
			patchDoc.ApplyTo(turbineToPatch, ModelState);

			if (!TryValidateModel(turbineToPatch))
			{
				return ValidationProblem(ModelState);
			}

			_mapper.Map(turbineToPatch, turbineEntity);
			_service.UpdateTurbine(turbineEntity);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteTurbine(int id)
		{
			var turbineModelFromRepo = _service.GetTurbineById(id);
			if (turbineModelFromRepo == null)
			{
				return NotFound();
			}

			var turbineEntity = _mapper.Map<Turbine>(turbineModelFromRepo);
			_service.DeleteTurbine(turbineEntity);

			return NoContent();
		}
	}
}
