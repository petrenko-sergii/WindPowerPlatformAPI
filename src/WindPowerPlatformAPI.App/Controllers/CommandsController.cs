using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommandsController : ControllerBase
	{
		private readonly ICommandService _service;

		public CommandsController(ICommandService service)
        {
			_service = service;
        }

		[HttpGet]
		public ActionResult<IEnumerable<Command>> GetAllCommands()
		{
			var commands = _service.GetAllCommands();

			return Ok(commands);
		}

		[HttpGet("{id}")]
		public ActionResult<Command> GetCommandById(int id)
		{
			var command = _service.GetCommandById(id);

			if (command == null)
			{
				return NotFound();
			}

			return Ok(command);
		}
	}
}
