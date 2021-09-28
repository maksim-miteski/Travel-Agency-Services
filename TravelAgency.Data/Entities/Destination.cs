using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Data.Entities
{
    [Table("Destination")]
    public class Destination 
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int AgentId { get; set; }
        public int AirplaneId { get; set; }
        public string City { get; set; }
        public DateTime DepartureDate { get; set; }

        //Navigating the passenger and agent classes
        [ForeignKey("PassengerId")]
        public Passenger Passenger { get; set; }
        [ForeignKey("AgentId")]
        public Agent Agent { get; set; }
        [ForeignKey("AirplaneId")]
        public Airplane Airplane { get; set; }

    }
}
