namespace Web_Lab1.Data
{
    public class DataSet
    {
        public static List<DogShelter> DogShelters = new List<DogShelter>
        {
            new DogShelter { Id = 1, Name = "Patron", Address = "123 Shevchenko Boulevard, Kyiv, Ukraine, 01601",
                ContactNumber = "+380682346578" },
            new DogShelter { Id = 2, Name = "Pesyk Center", Address = "45 Tarasivska Street, Kyiv, Ukraine, 02000",
                ContactNumber = "+380974140078" },
            new DogShelter { Id = 3, Name = "Dog Adoption Center", Address = "78 Lvivska Avenue, Kyiv, Ukraine, 03170",
                ContactNumber = "+380544326948" },
        };

        public static List<Dog> Dogs = new List<Dog>
        {
            new Dog { Id = 1, Breed = "Beagle", Age = 5, Weight = 14.5, IsAvailableForAdoption = false, ShelterId = 1, },
            new Dog { Id = 2, Breed = "Bulldog", Age = 2, Weight = 12, IsAvailableForAdoption = true, ShelterId = 1 },
            new Dog { Id = 3, Breed = "Greyhound", Age = 12, Weight = 20.8, IsAvailableForAdoption = true, ShelterId = 2 },
        };
    }
}
