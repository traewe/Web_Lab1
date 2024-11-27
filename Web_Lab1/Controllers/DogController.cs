using Microsoft.AspNetCore.Mvc;
using Web_Lab2.Entities;

namespace Web_Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogController : ControllerBase
    {
        private readonly List<Dog> _dogs;

        public DogController()
        {
            _dogs = DataSet.dogs;
        }

        /// <summary>
        /// Returns a list of all dogs
        /// </summary>
        /// <returns>All created dogs</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet(Name = "GetAllDogs")]
        public IEnumerable<Dog> GetAll()
        {
            return _dogs;
        }

        /// <summary>
        /// Returns a dog by id
        /// </summary>
        /// <param name="id">The id of dog that is searched</param>
        /// <returns>Dog by id</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{id:int:min(1)}", Name = "GetDogById")]
        public IActionResult Get(int id)
        {
            var dog = _dogs.FirstOrDefault(dog => dog.Id == id);

            return dog != null ? Ok(dog) : NotFound();
        }

        /// <summary>
        /// Creates a new dog
        /// </summary>
        /// <param name="dog">New dog</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [HttpPost(Name = "AddDog")]
        public IActionResult Post([FromBody] Dog dog)
        {
            var validationError = ValidateDog(dog);

            if (validationError != null)
            {
                return validationError;
            }

            dog.Id = DataSet.maxDogId + 1;
            DataSet.maxDogId++;

            _dogs.Add(dog);

            return CreatedAtAction(nameof(Get), new { id = dog.Id }, dog);
        }

        /// <summary>
        /// Changes the dog by id
        /// </summary>
        /// <param name="id">The id of dog that has to be changed</param>
        /// <param name="dog">Changed dog</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [HttpPut("{id:int:min(1)}", Name = "ChangeDog")]
        public IActionResult Put(int id, Dog dog)
        {
            var alreadyCreatedDog = _dogs.FirstOrDefault(dog => dog.Id == id);

            if (alreadyCreatedDog == null)
            {
                return NotFound();
            }

            var validationError = ValidateDog(dog);

            if (validationError != null)
            {
                return validationError;
            }

            alreadyCreatedDog.Name = dog.Name;
            alreadyCreatedDog.Breed = dog.Breed;
            alreadyCreatedDog.Age = dog.Age;
            alreadyCreatedDog.Weight = dog.Weight;
            alreadyCreatedDog.IsAvailableForAdoption = dog.IsAvailableForAdoption;
            alreadyCreatedDog.ShelterId = dog.ShelterId;

            return NoContent();
        }

        /// <summary>
        /// Deletes a dog by id
        /// </summary>
        /// <param name="id">The id of dog that has to be deleted</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{id:int:min(1)}", Name = "DeleteDog")]
        public IActionResult Delete(int id)
        {
            var dog = _dogs.FirstOrDefault(dog => dog.Id == id);

            if (dog == null)
            {
                return NotFound();
            }

            _dogs.Remove(dog);

            return NoContent();
        }

        private IActionResult ValidateDog(Dog dog)
        {
            if (dog == null)
            {
                return NotFound();
            }

            if (dog.ShelterId <= 0 || DataSet.dogShelters.Count < dog.ShelterId || DataSet.dogShelters[dog.ShelterId == null ? 0 : Convert.ToInt32(dog.ShelterId) - 1] == null)
            {
                return NotFound($"Dog shelter with ID {dog.ShelterId} was not found.");
            }

            if (dog.Age <= 0)
            {
                return BadRequest("Age is not acceptable.");
            }

            if (dog.Weight <= 0)
            {
                return BadRequest("Weight is not acceptable.");
            }

            if (string.IsNullOrEmpty(dog.Name) || dog.Name == "string")
            {
                return BadRequest("Name is not acceptable.");
            }

            if (string.IsNullOrEmpty(dog.Breed) || dog.Breed == "string")
            {
                return BadRequest("Breed is not acceptable.");
            }

            return null;
        }
    }
}
