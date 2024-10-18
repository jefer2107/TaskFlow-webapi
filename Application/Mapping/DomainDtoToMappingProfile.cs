using Application.DTOs;
using Application.DTOs.Category;
using Application.DTOs.Chore;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class DomainDtoToMappingProfile : Profile
{
    public DomainDtoToMappingProfile()
    {
        CreateMap<EntityModel, EntityModelDTO>().ReverseMap();
        
        CreateMap<User, UserDTO>()
        .ForMember(dest => dest.Chores, opt => opt.MapFrom(src => src.Chores))
        .ReverseMap();

        CreateMap<User, UserInputCreateDTO>().ReverseMap();
        CreateMap<User, UserInputUpdateDTO>().ReverseMap();
        CreateMap<User, UserInputAuthenticateDTO>().ReverseMap();

        CreateMap<Chore, ChoreDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>()
        .ForMember(dest => dest.Chores, opt => opt.MapFrom(src => src.Chores))
        .ReverseMap();
    }
}
