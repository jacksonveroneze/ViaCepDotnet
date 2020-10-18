using AutoMapper;
using JacksonVeroneze.ViaCep.Domain.Dto;
using JacksonVeroneze.ViaCep.Domain.Entities;

namespace JacksonVeroneze.ViaCep.API.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cep, SearchDataResult>();
        }
    }
}