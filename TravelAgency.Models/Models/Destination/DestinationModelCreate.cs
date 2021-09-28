using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgency.Models.Models.Destination
{
    public class DestinationModelCreate
    {
        [Required]
        public string City { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public int AgentId { get; set; }
        [Required]
        public int AirplaneId { get; set; }
        [Required]
        public int PassengerId { get; set; }
    }
}
