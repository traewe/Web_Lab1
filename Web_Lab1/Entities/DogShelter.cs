namespace Web_Lab2.Entities
{
    public class DogShelter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public ICollection<Dog> Dogs { get; set; }
    }
}
