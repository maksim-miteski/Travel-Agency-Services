using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelAgency.Models.Models.Agent
{
    public class AgentCreateModel
    {
        [DisplayName("Agent Name")]
        [Required(ErrorMessage = "Agent's Name is required!")]
        public string Name { get; set; }
        [DisplayName("Agent Last Name")]
        [Required(ErrorMessage = "Agent's Last Name is required!")]
        public string LastName { get; set; }
        [DisplayName("Agent Title")]
        [Required(ErrorMessage ="You must enter the titel of the Agent!")]
        public string Title { get; set; }
        [DisplayName("Agent's date of birth")]
        [Required(ErrorMessage = "Agent's Date of birth is required!")]
       
        public DateTime DoB { get; set; }
    }
}
