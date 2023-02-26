using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TurbinesController : ControllerBase
	{
		private readonly ITurbineService _service;

		public TurbinesController(ITurbineService service)
        {
			_service = service;
        }

		[HttpGet]
		public ActionResult<IEnumerable<Turbine>> GetAllTurbines()
		{
			var turbines = _service.GetAllTurbines();

			return Ok(turbines);
		}

		[HttpGet("{id}")]
		public ActionResult<Turbine> GetTurbineById(int id)
		{
			var turbine = _service.GetTurbineById(id);

			if (turbine == null)
			{
				return NotFound();
			}

			return Ok(turbine);
		}
	}
}
