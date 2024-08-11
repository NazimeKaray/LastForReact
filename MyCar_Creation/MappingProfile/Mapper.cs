using AutoMapper;
using MyCar_Creation.Dtos;
using MyCar_Creation.Entities;

namespace MyCar_Creation.MappingProfile
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Vehicle, VehicleDTO>().ReverseMap();
        }
    }
}
