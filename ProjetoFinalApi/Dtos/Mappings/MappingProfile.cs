using AutoMapper;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Time, TimeDTO>().ReverseMap();
        CreateMap<Jogador, JogadorDTO>().ReverseMap();
    }
}
