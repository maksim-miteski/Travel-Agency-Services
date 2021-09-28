using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TravelAgency.Data.Entities;
using TravelAgency.Models.Models.Airplane;

namespace TravelAgency.Models.Profiles
{
    public class AirplaneProfile
        :Profile
    {
        public AirplaneProfile()
        {
            CreateMap<Airplane, AirplaneModelBase>().ReverseMap();
            CreateMap<Airplane, AirplaneModelExtended>();
            CreateMap<AirplaneModelCreate, Airplane>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(Destination => Destination.Destinations, opt => opt.Ignore());
            CreateMap<AirplaneModelUpdate, Airplane>()
             .ForMember(Destination => Destination.Destinations, opt => opt.Ignore());

        }
    }
}
