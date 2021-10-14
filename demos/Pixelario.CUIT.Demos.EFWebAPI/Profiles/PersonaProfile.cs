using AutoMapper;
using Pixelario.CUIT.Demos.EFWebAPI.Dtos;
using Pixelario.CUIT.Demos.EFWebAPI.Models;

namespace Pixelario.CUIT.Demos.EFWebAPI.Profiles
{
    public class PersonaProfile : Profile
    {
        public PersonaProfile()
        {
            CreateMap<CreatePersonaDto, Persona>()
                .ForMember(dest => dest.CUIT, opt =>opt.MapFrom(src=> CUIT.Parse(src.CUIT)));
            CreateMap<Persona, PersonaDto>()
                .ForMember(dest => dest.CUIT, opt => opt.MapFrom(src => src.CUIT.ToString()));
        }
    }
}
