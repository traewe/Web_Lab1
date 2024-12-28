namespace Web_Lab2.Entities
{
    public class Dog
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