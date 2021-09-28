using Bogus;
using FluentAssertions;
using IS_FinalProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Agent;
using TravelAgency.Models.Models.Airplane;
using TravelAgency.Models.Models.Destination;
using TravelAgency.Models.Models.Passenger;
using TravelAgency.Services.Abstraction;
using Xunit;


namespace Stats.Controller.Test
{
    public class AgentControllerShould
    {
        private readonly Mock<IAgentService> _mockService;
        private readonly AgentController _controller;

        public AgentControllerShould()
        {
            _mockService = new Mock<IAgentService>();
            _controller = new AgentController(_mockService.Object);
        }

        [Fact]
        public async Task ReturnExtendedAgentByIdWhenHasData()
        {
            //Arrange
            var expectedId = 1;

            var airplanes = new Faker<AirplaneModelBase>()
               .RuleFor(a => a.Id, v => ++v.IndexVariable)
               .RuleFor(a => a.FlightNumber, v => v.Random.Number(111))
               .RuleFor(a => a.maxCapacity, v => v.Random.Number(120));

            var passengers = new Faker<PassengerModelBase>()
                .RuleFor(p => p.Id, v => ++v.IndexVariable)
                 .RuleFor(p => p.Name, v => v.Name.FullName())
                 .RuleFor(p => p.LastName, v => v.Name.FullName())
                 .RuleFor(p => p.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(p => p.passportNumber, v => v.Random.Number(1231450));

            var destinations = new Faker<DestinationModelBase>()
               .RuleFor(d => d.Id, v => ++v.IndexVariable)
               .RuleFor(d => d.City, v => v.Name.FullName())
               .RuleFor(d => d.DepartureDate, v => DateTime.Today);

            var agents = new Faker<AgentModelExtended>()
                 .RuleFor(a => a.Id, v => v.IndexVariable)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate(6);

            _mockService.Setup(x => x.Get(It.IsAny<int>()))
               .ReturnsAsync(agents.Find(x => x.Id == expectedId))
               .Verifiable();
            
            //Act
            var result = await _controller.GetAgent(expectedId);
            //Assert
            result.Should().BeOfType<OkObjectResult>();

            var model = result as OkObjectResult;
            model?.Value.Should().BeOfType<AgentModelExtended>().Subject.Id.Should().Be(expectedId);
        }

        [Fact]
        public async Task ReturnNoContentWhenHasNoData()
        {
            //Arrange
            var expectedId = 1;

            var airplanes = new Faker<AirplaneModelBase>()
               .RuleFor(a => a.Id, v => ++v.IndexVariable)
               .RuleFor(a => a.FlightNumber, v => v.Random.Number(111))
               .RuleFor(a => a.maxCapacity, v => v.Random.Number(120));

            var passengers = new Faker<PassengerModelBase>()
                .RuleFor(p => p.Id, v => ++v.IndexVariable)
                 .RuleFor(p => p.Name, v => v.Name.FullName())
                 .RuleFor(p => p.LastName, v => v.Name.FullName())
                 .RuleFor(p => p.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(p => p.passportNumber, v => v.Random.Number(1231450));

            var destinations = new Faker<DestinationModelBase>()
               .RuleFor(d => d.Id, v => ++v.IndexVariable)
               .RuleFor(d => d.City, v => v.Name.FullName())
               .RuleFor(d => d.DepartureDate, v => DateTime.Today);

            var agents = new Faker<AgentModelExtended>()
                 .RuleFor(a => a.Id, v => v.IndexVariable)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate(6);

            _mockService.Setup(x => x.Get(It.IsAny<int>()))
               .ReturnsAsync(agents.Find(x => x.Id == expectedId))
               .Verifiable();

            //Act
            var result = await _controller.GetAgent(expectedId);
            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ReturnAgentsWhenHasData()
        {
            //Arrange
            var expectedCount = 6;

            var agents = new Faker<AgentModelBase>()
                 .RuleFor(a => a.Id, v => v.IndexVariable)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate(6);

            _mockService.Setup(x => x.Get())
               .ReturnsAsync(agents)
               .Verifiable();

            //Act
            var result = await _controller.GetAllAgents();
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var model = result as OkObjectResult;
            model?.Value.Should().BeOfType<List<AgentModelBase>>().Subject.Count().Should().Be(expectedCount);
        }

        [Fact]
        public async Task ReturnEmptyListWhenNoData()
        {
            //Arrange
            var agents = new Faker<AgentModelBase>()
                 .RuleFor(a => a.Id, v => v.IndexVariable)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate(0);

            _mockService.Setup(x => x.Get())
               .ReturnsAsync(agents)
               .Verifiable();

            //Act
            var result = await _controller.GetAllAgents();
            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ReturnCreatedAgentOnCreateWhenCorrectModel()
        {
            //Arrange

            var agent = new Faker<AgentCreateModel>()
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            var expect = new Faker<AgentModelBase>()
                 .RuleFor(a => a.Id,1)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<AgentCreateModel>())).ReturnsAsync(expect);

            //Act
            var result = await _controller.Post(agent);
            //Assert
            result.Should().BeOfType<CreatedResult>();
            var model = result as CreatedResult;
            model?.Value.Should().Be(1);
            model?.Location.Should().Be("/api/Agents/1");
        }

        [Fact]
        public async Task ReturnConflictOnCreateWhenRepositoryError()
        {
            //Arrange

            var agent = new Faker<AgentCreateModel>()
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<AgentCreateModel>())).ReturnsAsync(()=>null);

            //Act
            var result = await _controller.Post(agent);
            //Assert
            result.Should().BeOfType<ConflictResult>();
        }

        [Fact]
        public async Task ReturnBadRequestOnCreateWhenNotValid()
        {
            //Arrange
            _controller.ModelState.AddModelError("fakeError","FakeError message");
            var agent = new Faker<AgentCreateModel>()
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            var expect = new Faker<AgentModelBase>()
                 .RuleFor(a => a.Id, 1)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            _mockService.Setup(x => x.Insert(It.IsAny<AgentCreateModel>())).ReturnsAsync(expect);

            //Act
            var result = await _controller.Post(agent);
            //Assert
            result.Should().BeOfType<BadRequestResult>();
      
        }

        [Fact]
        public async Task ReturnBadRequestOnUpdateWhenNotValid()
        {
            //Arrange
            _controller.ModelState.AddModelError("fakeError", "FakeError message");
            var agent = new Faker<AgentUpdateModel>()
                 .RuleFor(a => a.Id, 1)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            var expect = new Faker<AgentModelBase>()
                 .RuleFor(a => a.Id, 1)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            _mockService.Setup(x => x.Update(It.IsAny<AgentUpdateModel>())).ReturnsAsync(expect);

            //Act
            var result = await _controller.Put(agent.Id,agent);
            //Assert
            result.Should().BeOfType<BadRequestResult>();

        }

        [Fact]
        public async Task ReturnAgentOnUpdateWhenCorrectModel()
        {
            //Arrange

            var agent = new Faker<AgentUpdateModel>()
                 .RuleFor(a => a.Id, 1)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            var expect = new Faker<AgentModelBase>()
                 .RuleFor(a => a.Id, 1)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            _mockService.Setup(x => x.Update(It.IsAny<AgentUpdateModel>())).ReturnsAsync(expect);

            //Act
            var result = await _controller.Put(agent.Id, agent);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var model = result as OkObjectResult;
            model?.Value.Should().Be(expect);
           
        }

        [Fact]
        public async Task ReturnNoContentOnUpdateWhenRepositoryError()
        {
            //Arrange

            var agent = new Faker<AgentUpdateModel>()
                 .RuleFor(a => a.Id, 1)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            var expect = new Faker<AgentModelBase>()
                 .RuleFor(a => a.Id, 1)
                 .RuleFor(a => a.Name, v => v.Name.FullName())
                 .RuleFor(a => a.LastName, v => v.Name.FullName())
                 .RuleFor(a => a.DoB, v => v.Date.Between(DateTime.Today.AddYears(-18), DateTime.Today.AddYears(-23)))
                 .RuleFor(a => a.Title, v => v.Name.FullName())
                 .Generate();

            _mockService.Setup(x => x.Update(It.IsAny<AgentUpdateModel>())).ReturnsAsync(()=>null);

            //Act
            var result = await _controller.Put(agent.Id, agent);
            //Assert
            result.Should().BeOfType<NoContentResult>();

        }

        [Fact]
        public async Task ReturnOkWhenDeleteData()
        {
            //Arrange
            int id = 1;
            bool expected = true;
            _mockService.Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(expected);
             //Act
            var result = await _controller.Delete(id);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var model = result as OkObjectResult;
            model?.Value.Should().Be(expected);
        }

        [Fact]
        public async Task ReturnOkFalseWhenDeleteDataToDelete()
        {
            //Arrange
            int id = 1;
            bool expected = false;
            _mockService.Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(expected);
            //Act
            var result = await _controller.Delete(id);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var model = result as OkObjectResult;
            model?.Value.Should().Be(expected);
        }

        [Fact]
        public async Task ReturnBadRequestWhenDModelError()
        {
            //Arrange
            _controller.ModelState.AddModelError("fakeError", "FakeError message");
            //Act
            var result = await _controller.Delete(1);
            //Assert
            result.Should().BeOfType<BadRequestResult>();
           
            
        }
    }
}
