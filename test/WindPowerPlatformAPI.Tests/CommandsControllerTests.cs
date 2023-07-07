using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using WindPowerPlatformAPI.App.Controllers;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.App.AutoMapper;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace WindPowerPlatformAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandService> mockService;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        ILogger<CommandsController> logger;

        //the class constructor will be called for every test
        public CommandsControllerTests()
        {
            mockService = new Mock<ICommandService>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
            logger = new Mock<ILogger<CommandsController>>().Object;
        }

        [Fact]
        public void GetCommandItems_WhenDBIsEmpty_Returns200OK()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllCommands()).Returns(GetCommands(0));
            var controller = GetCommandsController();

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandItems_WhenDBHasOneResource_ReturnsOneItem()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllCommands()).Returns(GetCommands(1));
            var controller = GetCommandsController();

            //Act
            var result = controller.GetAllCommands();

            //Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetCommandItems_WhenDBHasOneResource_Returns200OK()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllCommands()).Returns(GetCommands(1));
            var controller = GetCommandsController();

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandItems_WhenDBHasOneResource_ReturnsCorrectType()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllCommands()).Returns(GetCommands(1));
            var controller = GetCommandsController();

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }

        [Fact]
        public void GetCommandByID_WhenValidIDProvided_Returns200OK()
        {
            //Arrange
            var command = new CommandReadDto
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            };

            var controller = GetCommandsController();
            SetHttpContextMock(controller, command);

            //Act
            var result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void CreateCommand_WhenValidObjectSubmitted_ReturnsCorrectResponse()
        {
            //Arrange 
            var commandCreateDto = new CommandCreateDto()
            {
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            };

            mockService.Setup(svc => svc.CreateCommand(commandCreateDto))
                .Returns(new CommandReadDto
                {
                    Id = 5,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });

            var controller = GetCommandsController();

            //Act
            var result = controller.CreateCommand(commandCreateDto);

            //Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void DeleteCommand_WhenValidResourceIDSubmitted_Returns200OK()
        {
            //Arrange 
            var command = new CommandReadDto
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            };

            var controller = GetCommandsController();
            SetHttpContextMock(controller, command);

            //Act
            var result = controller.DeleteCommand(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        public void Dispose()
        {
            mockService = null;
            mapper = null;
            configuration = null;
            realProfile = null;
            logger = null;
        }

        private CommandsController GetCommandsController()
        {
            return new CommandsController(mockService.Object, mapper, logger);
        }

        private List<CommandReadDto> GetCommands(int num)
        {
            var commands = new List<CommandReadDto>();
            if (num > 0)
            {
                commands.Add(new CommandReadDto
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return commands;
        }

        private static void SetHttpContextMock(CommandsController controller, CommandReadDto command)
        {
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Items["command"] = command;
        }
    }
}
