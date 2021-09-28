using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Data.Entities;
using TravelAgency.Models.Models.Destination;

namespace TravelAgency.Models.Profiles
{
    public class DestinationProfile
        :Profile
    {
        public DestinationProfile()
        {
            CreateMap<Destination, DestinationModelBase>().ReverseMap();
            CreateMap<DestinationModelCreate, Destination>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(Destination => Destination.Passenger, opt => opt.Ignore())
              .ForMember(dest => dest.Agent, opt => opt.Ignore())
              .ForMember(Destination => Destination.Airplane, opt => opt.Ignore());
            CreateMap<DestinationModelUpdate, Destination>()
              .ForMember(Destination => Destination.Passenger, opt => opt.Ignore())
              .ForMember(dest => dest.Agent, opt => opt.Ignore())
              .ForMember(Destination => Destination.Airplane, opt => opt.Ignore());
        }
    }
}
