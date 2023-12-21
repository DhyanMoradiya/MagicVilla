using AutoMapper;
using MagicVilla_Web.Models.Dto;


namespace MagicVilla_VillaApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
