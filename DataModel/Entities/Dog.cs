<<<<<<<< HEAD:Web_Lab1/Entities/Dog.cs
﻿namespace Web_Lab2.Entities
========
﻿namespace DataModel.Entities
>>>>>>>> 89e3a584e324b444283e4a848b999196e7502645:DataModel/Entities/Dog.cs
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public bool IsAvailableForAdoption { get; set;  }
        public int? ShelterId { get; set; }
<<<<<<<< HEAD:Web_Lab1/Entities/Dog.cs
========
        public DogShelter? DogShelter { get; set; }
>>>>>>>> 89e3a584e324b444283e4a848b999196e7502645:DataModel/Entities/Dog.cs
    }
}
