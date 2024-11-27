using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Web_Lab2.Dtos.Dog;
using Web_Lab2.Dtos.DogShelter;
using Web_Lab2.Entities;

namespace Web_Lab2.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Dog, DogOutputDto>().ReverseMap();
            CreateMap<DogCreateDto, Dog>().ReverseMap();

            CreateMap<DogShelter, DogShelterOutputDto>().ReverseMap();
            CreateMap<DogShelterCreateDto, DogShelter>().ReverseMap();
        }
    }
}
