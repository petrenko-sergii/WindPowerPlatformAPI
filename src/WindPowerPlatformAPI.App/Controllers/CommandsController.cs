using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WindPowerPlatformAPI.Infrastructure.Dtos;
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
		public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
		{
			var commands = _service.GetAllCommands();

			return Ok(commands);
		}

		[HttpGet("{id}", Name="GetCommandById")]
		public ActionResult<CommandReadDto> GetCommandById(int id)
		{
			var command = _service.GetCommandById(id);

			if (command == null)
			{
				return NotFound();
			}

			return Ok(command);
		}

		[HttpPost]
		public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
		{
			if(commandCreateDto == null)
            {
				throw new ArgumentNullException(nameof(commandCreateDto));
			}

			var createdCommand = _service.CreateCommand(commandCreateDto);

			return CreatedAtRoute(nameof(GetCommandById), new { Id = createdCommand.Id }, createdCommand);
		}
	}
}
