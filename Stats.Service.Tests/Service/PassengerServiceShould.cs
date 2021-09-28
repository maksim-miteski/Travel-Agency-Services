using AutoMapper;
using FluentAssertions;
using Stats.Service.Tests.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Models.Models.Passenger;
using TravelAgency.Models.Profiles;
using TravelAgency.Services.Services;
using Xunit;

namespace Stats.Service.Tests.Service
{
    public class PassengerServiceShould : SqlLiteContext
    {
        private readonly IMapper _mapper;
        private readonly PassengerService _service;

        public PassengerServiceShould()
            : base(true)
        {
            if (_mapper == null)
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(AgentProfile));
                }).CreateMapper();
                _mapper = mapper;
            }
            _service = new PassengerService(DbContext, _mapper);
        }

        [Fact]
        public async Task GetPassengerById()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.Get(expected);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<PassengerModelExtended>();
            result.Id.Should().Be(expected);
        }

        [Fact]
        public async Task GetAllPassengers()
        {
            //Arrange
            var expected = 1;
            //Act
            var result = await _service.Get();
            //Assert
            result.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(expected);
            result.Should().BeAssignableTo<PassengerModelExtended>();
        }

        [Fact]
        public async Task InsertNewPassenger()
        {
            //Arrange
            var passenger = new PassengerModelCreate
            {
                Name = "Passenger's Name",
                LastName = "Passengers's LastName",
                DoB = DateTime.Today,
                passportNumber=1234567
            };
            //Act
            var result = await _service.Insert(passenger);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<PassengerModelBase>();
            result.Should().NotBe(0);
        }

        [Fact]
        public async Task UpdatePassenger()
        {
            //Arrange
            var passenger = new PassengerModelUpdate
            {
                Id = 1,
                Name = "Passenger's Name",
                LastName = "Passengers's LastName",
                DoB = DateTime.Today,
                passportNumber = 1234567
            };
            //Act
            var result = await _service.Update(passenger);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<PassengerModelBase>();
            result.Id.Should().Be(passenger.Id);
            result.Name.Should().Be(passenger.Name);
            result.LastName.Should().Be(passenger.LastName);
            result.DoB.Should().Be(passenger.DoB);
            result.passportNumber.Should().Be(passenger.passportNumber);
        }

        [Fact]
        public async Task DeletePassenger()
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
