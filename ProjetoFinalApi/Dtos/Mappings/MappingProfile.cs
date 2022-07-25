using AutoMapper;
using ProjetoFinalApi.Dtos;
using ProjetoFinalApi.Models.Data;

namespace ProjetoFinalApi.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Time, TimeDTO>().ReverseMap();

        }
    }
}
