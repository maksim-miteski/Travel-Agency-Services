using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TravelAgency.Models.Models.Destination;

namespace TravelAgency.Models.Models.Passenger
{
    public class PassengerModelExtended
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DoB { get; set; }
        [Required]
        public int passportNumber { get; set; }

        public virtual IEnumerable<DestinationModelBase> Destinations { get; set; }

    }
}
