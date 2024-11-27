using Web_Lab2.Dtos.DogShelter;

namespace Web_Lab2.Dtos.Dog
{
    public class DogOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public bool IsAvailableForAdoption { get; set; }
        public int? ShelterId { get; set; }
    }
}
