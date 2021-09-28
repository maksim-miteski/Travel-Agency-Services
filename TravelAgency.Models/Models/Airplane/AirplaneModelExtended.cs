using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TravelAgency.Models.Models.Destination;

namespace TravelAgency.Models.Models.Airplane
{
    public class AirplaneModelExtended
    {
        public int Id { get; set; }
        [Required]
        public int FlightNumber { get; set; }
        [Required]
        public int maxCapacity { get; set; }

        public virtual IEnumerable<DestinationModelBase> Destinations { get; set; }

    }
}
