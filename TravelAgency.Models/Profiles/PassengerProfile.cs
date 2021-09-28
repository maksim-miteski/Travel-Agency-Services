using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Data.Entities;
using TravelAgency.Models.Models.Passenger;

namespace TravelAgency.Models.Profiles
{
    public class PassengerProfile
        :Profile
    {
        public PassengerProfile()
        {
            CreateMap<Passenger, PassengerModelBase>().ReverseMap();
            CreateMap<Passenger, PassengerModelExtended>();
            CreateMap<PassengerModelCreate, Passenger>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(Destination => Destination.Destinations, opt => opt.Ignore());
            CreateMap<PassengerModelUpdate, Passenger>()
             .ForMember(Destination => Destination.Destinations, opt => opt.Ignore());

        }
    }
}
