<<<<<<<< HEAD:Web_Lab1/Entities/DogShelter.cs
namespace Web_Lab2.Entities
========
namespace DataModel.Entities
>>>>>>>> 89e3a584e324b444283e4a848b999196e7502645:DataModel/Entities/DogShelter.cs
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
