using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommandsController : ControllerBase
	{
		private readonly ICommandService _service;
		private readonly IMapper _mapper;

		public CommandsController(ICommandService service, IMapper mapper)
        {
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
		{
			var commands = _service.GetAllCommands();

			return Ok(commands);
		}

        [Authorize]
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


		[HttpPut("{id}")]
		public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
		{
			var commandModelFromRepo = _service.GetCommandById(id);
			if (commandModelFromRepo == null)
			{
				return NotFound();
			}

			var commandEntity = _mapper.Map<Command>(commandModelFromRepo);
			_mapper.Map(commandUpdateDto, commandEntity);

			_service.UpdateCommand(commandEntity);

			return NoContent();
		}

		[HttpPatch("{id}")]
		public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
		{
			var commandModelFromRepo = _service.GetCommandById(id);
			if (commandModelFromRepo == null)
			{
				return NotFound();
			}

			var commandEntity = _mapper.Map<Command>(commandModelFromRepo);

			var commandToPatch = _mapper.Map<CommandUpdateDto>(commandEntity);
			patchDoc.ApplyTo(commandToPatch, ModelState);

			if (!TryValidateModel(commandToPatch))
			{
				return ValidationProblem(ModelState);
			}

			_mapper.Map(commandToPatch, commandEntity);
			_service.UpdateCommand(commandEntity);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteCommand(int id)
        {
			var commandModelFromRepo = _service.GetCommandById(id);
			if (commandModelFromRepo == null)
			{
				return NotFound();
			}

			var commandEntity = _mapper.Map<Command>(commandModelFromRepo);
			_service.DeleteCommand(commandEntity);

			return NoContent();
		}
	}
}
