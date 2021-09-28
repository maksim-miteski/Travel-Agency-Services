using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgency.Models.Models.Airplane
{
    public class AirplaneModelUpdate
    {
        [DisplayName("Flight ID")]
        [Required(ErrorMessage ="Airplane id is required in order to update the airplane!")]
        public int Id { get; set; }
        [DisplayName("Flight Number")]
        [Required(ErrorMessage = "Flight Number is required!")]
    
        public int FlightNumber { get; set; }
        [DisplayName("Passengers maximum capacity")]
        [Required(ErrorMessage = "MaxCapacity is required!")]
       
        public int maxCapacity { get; set; }
    }
}
