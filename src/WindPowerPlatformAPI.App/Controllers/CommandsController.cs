using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Attributes;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;

namespace WindPowerPlatformAPI.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommandsController : BaseApiController
	{
		private readonly ICommandService _service;

		public CommandsController(ICommandService service, IMapper mapper, ILogger<CommandsController> logger)
			: base(mapper, logger) 
		{
			_service = service;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
		{
			_logger.LogInformation("Method \"GetAllCommands\" was called.");
			var commands = _service.GetAllCommands();

			return Ok(commands);
		}

		[Authorize]
		[HttpGet("{id}", Name="GetCommandById")]
		[ServiceFilter(typeof(CommandExistsValidationAttribute<CommandBaseDto>))]
		public ActionResult<CommandReadDto> GetCommandById(int id)
		{
			var command = HttpContext.Items["command"] as CommandReadDto;

			return Ok(command);
		}

		[HttpPost]
		[Consumes("application/json")]
		[ServiceFilter(typeof(CommandValidationFilterAttribute))]
		public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
		{
			var createdCommand = _service.CreateCommand(commandCreateDto);

			return CreatedAtRoute(nameof(GetCommandById), new { Id = createdCommand.Id }, createdCommand);
		}


		[HttpPut("{id}")]
		[Consumes("application/json")]
		[ServiceFilter(typeof(CommandValidationFilterAttribute))]
		[ServiceFilter(typeof(CommandExistsValidationAttribute<CommandBaseDto>))]
		public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
		{
			var commandModelFromRepo = HttpContext.Items["command"] as CommandReadDto;

			var commandEntity = _mapper.Map<Command>(commandModelFromRepo);
			_mapper.Map(commandUpdateDto, commandEntity);

			_service.UpdateCommand(commandEntity);

			return NoContent();
		}

		[HttpPatch("{id}")]
		[ServiceFilter(typeof(CommandExistsValidationAttribute<CommandBaseDto>))]
		public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
		{
			var commandModelFromRepo = HttpContext.Items["command"] as CommandUpdateDto;

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
		[ServiceFilter(typeof(CommandExistsValidationAttribute<CommandBaseDto>))]
		public ActionResult DeleteCommand(int id)
		{
			var commandModelFromRepo = HttpContext.Items["command"] as CommandReadDto;

			var commandEntity = _mapper.Map<Command>(commandModelFromRepo);
			_service.DeleteCommand(commandEntity);

			return NoContent();
		}
	}
}
