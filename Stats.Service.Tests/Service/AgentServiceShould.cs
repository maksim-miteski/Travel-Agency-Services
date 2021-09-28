using AutoMapper;
using FluentAssertions;
using Stats.Service.Tests.Internal;
using System;
using System.Collections;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Agent;
using TravelAgency.Models.Profiles;
using TravelAgency.Services.Services;
using Xunit;

namespace Stats.Service.Tests.Service
{
    public class AgentServiceShould : SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly AgentService _service;

        public AgentServiceShould()
            :base(true)
        {
            if(_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(AgentProfile));
                }).CreateMapper();
                _mapper = mapper;
            }
            _service = new AgentService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetAgentById()
        {
            //Arrange
            var expected = 1;
           //Act
            var result =await _service.Get(expected);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AgentModelExtended>();
            result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task GetAllAgents()
        {
            //Arrange
            var expected = 1;
            //Act
            var result =await  _service.Get();
            //Assert
            result.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<AgentModelExtended>();
        }

        [Fact]
        public async Task InsertNewAgent()
        {
            //Arrange
            var agent = new AgentCreateModel
            {
                Name = "Agent's Name",
                LastName = "Agent's LastName",
                Title = "Job Title",
                DoB = DateTime.Today
            };
            //Act
            var result =await _service.Insert(agent);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AgentModelBase>();
            result.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateAgent()
        {
            //Arrange
            var agent = new AgentUpdateModel
            {
                Id=1,
                Name = "Agent's Name",
                LastName = "Agent's LastName",
                Title = "Job Title",
                DoB = DateTime.Today
            };
            //Act
            var result = await _service.Update(agent);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AgentModelBase>();
            result.Id.Should().Be(agent.Id);
             result.Name.Should().Be(agent.Name);
            result.LastName.Should().Be(agent.LastName);
            result.Title.Should().Be(agent.Title);
            result.DoB.Should().Be(agent.DoB);


        }

        [Fact]
        public async Task DeleteAgent()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.Delete(expected);
            var agent = await _service.Get(expected);
            //Assert
            result.Should().Be(true);
            agent.Should().BeNull();


        }

    }
}
