using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Models.Models.Agent;
using TravelAgency.Models.Models.Airplane;
using TravelAgency.Models.Models.Passenger;

namespace TravelAgency.Models.Models.Destination
{
    public class DestinationModelBase
    {
        public int Id { get; set; }
        public string City { get; set; }
        public DateTime DepartureDate { get; set; }

        public AgentModelBase Agent { get; set; }
        public AirplaneModelBase Airplane { get; set; }
        public PassengerModelBase Passenger { get; set; }

    
    }
}
