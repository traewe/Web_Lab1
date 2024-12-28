namespace Web_Lab2.Entities
{
    public class City
    {
        public string Name { get; set; }
        public int NumberOfStrayDogs { get; set; }
        public static IEnumerable<City> GenerateRandom()
        {
            var cityNames = new[] { "Kyiv", "Lviv", "Odesa", "Kharkiv", "Dnipro" };

            var random = new Random();

            return Enumerable.Range(0, 5).Select(i => new City
            {
                Name = cityNames[i % cityNames.Length],
                NumberOfStrayDogs = random.Next(0, 1001)
            });
        }
    }
}
