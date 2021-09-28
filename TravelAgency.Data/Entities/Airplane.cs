using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelAgency.Data.Entities
{
    [Table("Airplane")]
    public class Airplane
    {
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public int maxCapacity { get; set; }

        /*[ForeignKey("PassengerId")]
        public Passenger Passenger { get; set; }
        [ForeignKey("AgentId")]
        public Agent Agent { get; set; }*/

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
