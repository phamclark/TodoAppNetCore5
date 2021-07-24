using AutoMapper;
using TodoApp.Models;
using TodoApp.Models.DTOs;

namespace TodoApp.Mapping
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}