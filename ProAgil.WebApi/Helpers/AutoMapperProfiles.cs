using System.Linq;
using AutoMapper;
using ProAgil.Domain;
using ProAgil.Domain.Identity;
using ProAgil.WebApi.Dtos;

namespace ProAgil.WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            
            CreateMap<Evento, EventoDto>()
            .ForMember(destino => destino.Palestrantes, opt => {
                opt.MapFrom(fonte => fonte.PalestrantesEventos.Select(x => x.Palestrante).ToList());
            }).ReverseMap();


            CreateMap<Palestrante, PalestranteDto>()
            .ForMember(destino => destino.Eventos, opt =>{
                opt.MapFrom(fonte => fonte.PalestrantesEventos.Select(x => x.Evento).ToList());
            }).ReverseMap();

            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();


        }
    }
}