using System;
using System.Collections.Generic;
using System.Text;

namespace TravelAgency.Models.Models.Passenger
{
    public class PassengerModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public int passportNumber { get; set; }
    }
}
