using Web_Lab2.Dtos.Dog;

namespace Web_Lab2.Dtos.DogShelter
{
    public class DogShelterOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<DogOutputDto> DogOutputDtos { get; set; }
    }
}
