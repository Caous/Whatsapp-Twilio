using AutoMapper;
using DomainDrivenDesign.Application.Entities.Response;
using DomainDrivenDesign.Domain.Entities;

namespace DomainDrivenDesign.Application.ResolveMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<MessagesResponseDto, GroupMongo>()
            .ForMember(p => p.Messages, opt => opt.MapFrom(src => src.Messages)).ReverseMap();
        CreateMap<GroupMessageResponseDto, GroupMongoMessage>().ReverseMap();

    }
}
