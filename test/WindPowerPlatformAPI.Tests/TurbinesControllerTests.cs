﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using WindPowerPlatformAPI.App.Controllers;
using WindPowerPlatformAPI.Infrastructure.Services.Interfaces;
using WindPowerPlatformAPI.Infrastructure.Dtos;
using WindPowerPlatformAPI.App.AutoMapper;
using Xunit;
using WindPowerPlatformAPI.Infrastructure.Helpers.Interfaces;
using Microsoft.Extensions.Logging;

namespace WindPowerPlatformAPI.Tests
{
    public class TurbinesControllerTests : IDisposable
    {
        Mock<ITurbineService> mockService;
        Mock<IAzureResponseHelper> mockResponseHelper;
        TurbinesProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        ILogger<TurbinesController> logger;
        TurbineReadDto testTurbineReadDto;

        //the class constructor will be called for every test
        public TurbinesControllerTests()
        {
            mockService = new Mock<ITurbineService>();
            mockResponseHelper = new Mock<IAzureResponseHelper>();
            realProfile = new TurbinesProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
            logger = new Mock<ILogger<TurbinesController>>().Object;
            testTurbineReadDto = new TurbineReadDto { Id = 1, SerialNumber = "MockNumber", Price = 100 };
        }

        [Fact]
        public void GetTurbineItems_WhenDBIsEmpty_Returns200OK()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllTurbines(string.Empty)).Returns(GetTurbines(0));
            var controller = GetTurbinesController();

            //Act
            var result = controller.GetAllTurbines(string.Empty);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetTurbineItems_WhenDBHasOneResource_ReturnsOneItem()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllTurbines(string.Empty)).Returns(GetTurbines(1));
            var controller = GetTurbinesController();

            //Act
            var result = controller.GetAllTurbines(string.Empty);

            //Assert
            var okResult = result.Result as OkObjectResult;
            var turbines = okResult.Value as List<TurbineReadDto>;
            Assert.Single(turbines);
        }

        [Fact]
        public void GetTurbineItems_WhenDBHasOneResource_Returns200OK()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllTurbines(string.Empty)).Returns(GetTurbines(1));
            var controller = GetTurbinesController();

            //Act
            var result = controller.GetAllTurbines(string.Empty);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetTurbineItems_WhenDBHasOneResource_ReturnsCorrectType()
        {
            //Arrange
            mockService.Setup(svc => svc.GetAllTurbines(string.Empty)).Returns(GetTurbines(1));
            var controller = GetTurbinesController();

            //Act
            var result = controller.GetAllTurbines(string.Empty);

            //Assert
            Assert.IsType<ActionResult<IEnumerable<TurbineReadDto>>>(result);
        }

        [Fact]
        public void GetTurbineByID_WhenNonExistentIDProvided_Returns404NotFound()
        {
            //Arrange
            mockService.Setup(svc => svc.GetTurbineById(0)).Returns(() => null);
            var controller = GetTurbinesController();

            //Act
            var result = controller.GetTurbineById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetTurbineByID_WhenValidIDProvided_Returns200OK()
        {
            //Arrange
            mockService.Setup(svc => svc.GetTurbineById(1))
                .Returns(testTurbineReadDto);
            var controller = GetTurbinesController();

            //Act
            var result = controller.GetTurbineById(1);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetTurbineByID_WhenValidIDProvided_ReturnsCorrectType()
        {
            //Arrange
            mockService.Setup(svc => svc.GetTurbineById(1))
                .Returns(testTurbineReadDto);
            var controller = GetTurbinesController();

            //Act
            var result = controller.GetTurbineById(1);

            //Assert
            Assert.IsType<ActionResult<TurbineReadDto>>(result);
        }

        [Fact]
        public void CreateTurbine_WhenValidObjectSubmitted_ReturnsCorrectResponse()
        {
            //Arrange 
            var turbineCreateDto = new TurbineCreateDto()
            {
                SerialNumber = "MockNumber",
                Price = 100
            };

            mockService.Setup(svc => svc.CreateTurbine(turbineCreateDto))
                .Returns(new TurbineReadDto
                {
                    Id = 5,
                    SerialNumber = "MockNumber",
                    Price = 100
                });

            var controller = GetTurbinesController();

            //Act
            var result = controller.CreateTurbine(turbineCreateDto);

            //Assert
            Assert.IsType<ActionResult<TurbineReadDto>>(result);
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateTurbine_WhenValidObjectSubmitted_Returns204NoContent()
        {
            //Arrange 
            mockService.Setup(svc => svc.GetTurbineById(1))
                .Returns(testTurbineReadDto);

            var controller = GetTurbinesController();

            //Act
            var result = controller.UpdateTurbine(1, new TurbineUpdateDto { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateTurbine_WhenNonExistentResourceIDSubmitted_Returns404NotFound()
        {
            //Arrange 
            mockService.Setup(svc => svc.GetTurbineById(0)).Returns(() => null);
            var controller = GetTurbinesController();

            //Act
            var result = controller.UpdateTurbine(0, new TurbineUpdateDto { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialTurbineUpdate_WhenNonExistentResourceIDSubmitted_Returns404NotFound()
        {
            //Arrange 
            mockService.Setup(svc => svc.GetTurbineById(0)).Returns(() => null);
            var controller = GetTurbinesController();

            //Act
            var result = controller.PartialTurbineUpdate(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<TurbineUpdateDto> { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteTurbine_WhenValidResourceIDSubmitted_Returns200OK()
        {
            //Arrange 
            mockService.Setup(svc => svc.GetTurbineById(1))
                    .Returns(testTurbineReadDto);

            var controller = GetTurbinesController();

            //Act
            var result = controller.DeleteTurbine(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteTurbine_WhenNonExistentResourceIDSubmitted_Returns_404NotFound()
        {
            //Arrange 
            mockService.Setup(svc => svc.GetTurbineById(0)).Returns(() => null);
            var controller = GetTurbinesController();

            //Act
            var result = controller.DeleteTurbine(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        public void Dispose()
        {
            mockService = null;
            mapper = null;
            configuration = null;
            realProfile = null;
            testTurbineReadDto = null;
            logger = null;
        }

        private TurbinesController GetTurbinesController()
        {
            return new TurbinesController(mockService.Object, mockResponseHelper.Object, null, mapper, logger);
        }

        private List<TurbineReadDto> GetTurbines(int num)
        {
            var turbines = new List<TurbineReadDto>();
            if (num > 0)
            {
                turbines.Add(testTurbineReadDto);
            }

            return turbines;
        }
    }
}
