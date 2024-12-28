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
            CreateMap<Dog, DogOutputDto>();
            CreateMap<DogCreateDto, Dog>();

            CreateMap<DogShelter, DogShelterOutputDto>();
            CreateMap<DogShelterCreateDto, DogShelter>();
        }
    }
}
