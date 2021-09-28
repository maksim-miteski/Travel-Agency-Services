using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Models.Models.Airplane
{
    public  class AirplaneModelBase
    {
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public int maxCapacity { get; set; }
    }
}
