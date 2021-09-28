using AutoMapper;
using FluentAssertions;
using Stats.Service.Tests.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Airplane;
using TravelAgency.Models.Profiles;
using TravelAgency.Services.Services;
using Xunit;

namespace Stats.Service.Tests.Service
{
   public class AirplaneServiceShould :SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly AirplaneService _service;

        public AirplaneServiceShould()
            : base(true)
        {
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(AirplaneProfile));
                }).CreateMapper();
                _mapper = mapper;
            }
            _service = new AirplaneService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetAirplaneById()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.Get(expected);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AirplaneModelExtended>();
            result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task GetAllAirplanes()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.Get();
            //Assert
            result.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<AirplaneModelExtended>();
        }

        [Fact]
        public async Task InsertNewAirplane()
        {
            //Arrange
            var passenger = new AirplaneModelCreate
            {
                FlightNumber=111,
                maxCapacity=120
            };
            //Act
            var result = await _service.Insert(passenger);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AirplaneModelBase>();
            result.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdateAirplane()
        {
            //Arrange
            var airplane = new AirplaneModelUpdate
            {
                Id = 1,
                FlightNumber = 111,
                maxCapacity = 120
            };
            //Act
            var result = await _service.Update(airplane);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<AirplaneModelBase>();
            result.Id.Should().Be(airplane.Id);
            result.FlightNumber.Should().Be(airplane.FlightNumber);
            result.maxCapacity.Should().Be(airplane.maxCapacity);
        }

        [Fact]
        public async Task DeleteAirplane()
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
