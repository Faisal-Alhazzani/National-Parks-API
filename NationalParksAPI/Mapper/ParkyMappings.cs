
using AutoMapper;
using NationalParksAPI.Models;
using NationalParksAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.ParkyMapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalParkEntity, NationalParkDTO>().ReverseMap();
            CreateMap<TrailEntity, TrailDTO>().ReverseMap();
            CreateMap<TrailEntity, TrailCreateDTO>().ReverseMap();
            CreateMap<TrailEntity, TrailUpdateDTO>().ReverseMap();
        }
    }
}