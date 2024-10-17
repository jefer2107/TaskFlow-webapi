using Application.DTOs;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class DomainDtoToMappingProfile : Profile
{
    public DomainDtoToMappingProfile()
    {
        CreateMap<EntityModel, EntityModelDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UserInputCreateDTO>().ReverseMap();
        CreateMap<User, UserInputUpdateDTO>().ReverseMap();
        CreateMap<User, UserInputAuthenticateDTO>().ReverseMap();
    }
}
