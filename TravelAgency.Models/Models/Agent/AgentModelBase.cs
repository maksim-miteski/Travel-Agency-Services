using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgency.Models.Models.Agent
{
    public class AgentModelBase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Title { get; set; }
        [DisplayName("Agent's date of birth")]
        public DateTime DoB { get; set; }
    }
}

