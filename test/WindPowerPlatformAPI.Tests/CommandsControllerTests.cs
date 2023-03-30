using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using WindPowerPlatformAPI.Domain.Entities;
using WindPowerPlatformAPI.App.Controllers;
using WindPowerPlatformAPI.Infrastructure.Data;
using Xunit;
using WindPowerPlatformAPI.Infrastructure.Data.Repositories.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.App.AutoMapper;

namespace WindPowerPlatformAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandService> mockService;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        //the class constructor will be called for every test
        public CommandsControllerTests()
        {
            mockService = new Mock<ICommandService>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        [Fact]
        public void GetCommandItems_WhenDBIsEmpty_Returns200OK()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllCommands()).Returns(GetCommands(0));
            var controller = new CommandsController(mockService.Object, mapper);

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
            var controller = new CommandsController(mockService.Object, mapper);

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
            var controller = new CommandsController(mockService.Object, mapper);

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
            var controller = new CommandsController(mockService.Object, mapper);

            //Act
            var result = controller.GetAllCommands();

            //Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }


        [Fact]
        public void GetCommandByID_WhenNonExistentIDProvided_Returns404NotFound()
        {
            //Arrange
            mockService.Setup(svc => svc.GetCommandById(0)).Returns(() => null);

            var controller = new CommandsController(mockService.Object, mapper);
           
            //Act
            var result = controller.GetCommandById(1);
            
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_WhenValidIDProvided_Returns200OK()
        {
            //Arrange
            mockService.Setup(svc => svc.GetCommandById(1))
                .Returns(new CommandReadDto
                {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });
            var controller = new CommandsController(mockService.Object, mapper);

            //Act
            var result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandByID_WhenValidIDProvided_ReturnsCorrectType()
        {
            //Arrange
            mockService.Setup(svc => svc.GetCommandById(1))
                .Returns(new CommandReadDto
                {
                    Id = 1,
                    HowTo = "mock",
                    Platform = "Mock",
                    CommandLine = "Mock"
                });
            var controller = new CommandsController(mockService.Object, mapper);

            //Act
            var result = controller.GetCommandById(1);

            //Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
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

        public void Dispose()
        {
            mockService = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }
    }
}
