using AutoMapper;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Time, TimeDTO>().ReverseMap();
        CreateMap<Jogador, JogadorDTO>().ReverseMap();
        CreateMap<Transferencia, TransferenciaDTO>().ReverseMap();
        CreateMap<Torneio, TorneioDTO>().ReverseMap();
        CreateMap<Partida, PartidaDTO>().ReverseMap();
        CreateMap<Partida, NovaPartidaDTO>().ReverseMap();
        CreateMap<EventoPartida, EventoPartidaDTO>().ReverseMap();
    }
}
