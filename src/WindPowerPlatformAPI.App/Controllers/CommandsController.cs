using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.Infrastructure.Repositories.Interfaces;

namespace WindPowerPlatformAPI.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommandsController : ControllerBase
	{
		private readonly ICommandRepository _repository;

        public CommandsController(ICommandRepository repository)
        {
			_repository = repository;
        }

		[HttpGet]
		public ActionResult<IEnumerable<Command>> GetAllCommands()
		{
			var commandItems = _repository.GetAllCommands();

			return Ok(commandItems);
		}

		[HttpGet("{id}")]
		public ActionResult<Command> GetCommandById(int id)
		{
			var commandItem = _repository.GetCommandById(id);

			if (commandItem == null)
			{
				return NotFound();
			}

			return Ok(commandItem);
		}
	}
}
