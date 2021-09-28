using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Data.Entities;
using TravelAgency.Models.Models.Agent;

namespace TravelAgency.Models.Profiles
{
    public class AgentProfile
        :Profile
    {
        public AgentProfile()
        {
            CreateMap<Agent, AgentModelBase>().ReverseMap();//Linking the Agent Entity with the mase model
            CreateMap<Agent, AgentModelExtended>(); //Linking the Entity Agent with the Extended Model
            CreateMap<AgentCreateModel, Agent>() //Linking the Agent Model for creating new agent with the database for every element
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(Destination => Destination.Destinations, opt => opt.Ignore());
            CreateMap<AgentUpdateModel, Agent>() //Linking the Agent Model for Updating with the database for every element
             .ForMember(Destination => Destination.Destinations, opt => opt.Ignore());

        }
    }
}
