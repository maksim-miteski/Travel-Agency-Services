using StatsModels.Tests.Internal;
using System;
using TravelAgency.Models.Profiles;
using Xunit;

namespace StatsModels.Tests
{
    public class MappingConfigurationValidation
    {
        [Fact]
        public void IsValid()
        {
            //Arrange 
            var configuration = AutoMapperModule.CreateMapperConfiguration<AgentProfile>();
            //Act / Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
