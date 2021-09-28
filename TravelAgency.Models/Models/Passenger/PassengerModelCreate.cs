using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgency.Models.Models.Passenger
{
    public class PassengerModelCreate
    {
        [DisplayName("Passenger Name")]
        [Required(ErrorMessage = "Passenger's Name is required!")]
        public string Name { get; set; }
        [DisplayName("Passenger Last Name")]
        [Required(ErrorMessage = "Passenger's Last Name is required!")]
        public string LastName { get; set; }
        [DisplayName("Passenger Date of Birth")]
        [Required(ErrorMessage = "Passenger's Date of Birth is required!")]
    
        public DateTime DoB { get; set; }
        [DisplayName("Passenger's Passport Number")]
        //[MaxLength(10)]
        [Required(ErrorMessage = "Passenger's Passport Number is required!")]
        public int passportNumber { get; set; }
    }
}
