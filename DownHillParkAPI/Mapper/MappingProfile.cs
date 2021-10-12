using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompetitionPrizeRequest, CompetitionPrize>();
            CreateMap<CompetitionPrize, CompetitionPrizeRequest>();
            CreateMap<CompetitionRequest, Competition>();
            CreateMap<Competition, CompetitionRequest>();
            CreateMap<BikeRequest, Bike>();
            CreateMap<Bike, BikeRequest>();
            CreateMap<TeamRequest, Team>();
            CreateMap<Team, TeamRequest>();
            CreateMap<RegisterUser, User>().
                ForMember("UserName", opt => opt.MapFrom(src => src.Email));
            CreateMap<ResultRequest, CompetitionResult>()
                .ForMember("TotalTime", opt => opt.MapFrom(src => src.Time));
            CreateMap<LapRequest, Lap>();
        }
    }
}
