using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelAgency.Data.Entities
{
    [Table("Passenger")]
    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName {get;set;}
        public DateTime DoB { get; set; }
        public int passportNumber { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
