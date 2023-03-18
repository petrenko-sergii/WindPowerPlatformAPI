using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Domain.Entities;

namespace WindPowerPlatformAPI.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TurbinesController : ControllerBase
	{
		private readonly ITurbineService _service;
		private readonly IMapper _mapper;

		public TurbinesController(ITurbineService service, IMapper mapper)
        {
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult<IEnumerable<TurbineReadDto>> GetAllTurbines()
		{
			var turbines = _service.GetAllTurbines();

			return Ok(turbines);
		}

		[HttpGet("{id}", Name = "GetTurbineById")]
		public ActionResult<TurbineReadDto> GetTurbineById(int id)
		{
			var turbine = _service.GetTurbineById(id);

			if (turbine == null)
			{
				return NotFound();
			}

			return Ok(turbine);
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
	}
}
