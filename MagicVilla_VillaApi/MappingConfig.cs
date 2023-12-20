﻿using AutoMapper;
using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Model.Dto;

namespace MagicVilla_VillaApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
        }
    }
}
