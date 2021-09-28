using AutoMapper;
using FluentAssertions;
using Stats.Service.Tests.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Agent;
using TravelAgency.Models.Models.Airplane;
using TravelAgency.Models.Models.Destination;
using TravelAgency.Models.Models.Passenger;
using TravelAgency.Models.Profiles;
using TravelAgency.Services.Services;
using Xunit;

namespace Stats.Service.Tests.Service
{
    public class DestinationServiceShould : SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly DestinationService _service;

        public DestinationServiceShould()
            : base(true)
        {
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(DestinationProfile));
                }).CreateMapper();
                _mapper = mapper;
            }
            _service = new DestinationService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetAirplaneById()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.GetAirplaneById(expected);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AirplaneModelExtended>();
            // result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task GetPassengerById()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.GetPassengerById(expected);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<PassengerModelExtended>();
            // result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task GetAgentById()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.GetAgentById(expected);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AgentModelExtended>();
            // result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task InsertNewDestination()
        {
            //Arrange
            var destination = new DestinationModelCreate
            {
                City = "Rome",
                DepartureDate = DateTime.Today

            };
            //Act
            var result = await _service.Insert(destination);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<DestinationModelBase>();
            result.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateDestination()
        {
            //Arrange
            var destination = new DestinationModelUpdate
            {
                Id = 1,
                City = "Rome",
                DepartureDate = DateTime.Today
            };
            //Act
            var result = await _service.Update(destination);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AirplaneModelBase>();
            result.Id.Should().Be(destination.Id);
            result.City.Should().Be(destination.City);
            result.DepartureDate.Should().Be(destination.DepartureDate);
        }

        [Fact]
        public async Task DeleteDestination()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.Delete(expected);
           //Assert
            result.Should().Be(true);
           }
    }
}
