using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelAgency.Data.Entities
{
    [Table("Agent")]
    public class Agent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DoB { get; set; }

        /* [ForeignKey("PassengerId")]
         public Passenger Passenger { get; set; }
         [ForeignKey("AirplaneId")]
         public Airplane Airplane { get; set; } */

         public virtual ICollection<Destination> Destinations { get; set; }
    }
}
