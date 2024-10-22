using Microsoft.AspNetCore.Mvc;
using Web_Lab1.Data;

namespace Web_Lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogShelterController : ControllerBase
    {
        private readonly List<DogShelter> _dogShelters;

        public DogShelterController()
        {
            _dogShelters = DataSet.dogShelters;
        }

        /// <summary>
        /// Returns a list of all shelters
        /// </summary>
        /// <returns>All created shelters</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet(Name = "GetAllDogShelters")]
        public IEnumerable<DogShelter> GetAll()
        {
            return _dogShelters;
        }

        /// <summary>
        /// Returns a shelter by id
        /// </summary>
        /// <param name="id">The id of shelter that is searched</param>
        /// <returns>shelter by id</returns>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [HttpGet("{id:int:min(1)}", Name = "GetDogShelterById")]
        public IActionResult Get(int id)
        {
            var shelter = _dogShelters.FirstOrDefault(shelter => shelter.Id == id);

            return shelter != null ? Ok(shelter) : NotFound();
        }

        /// <summary>
        /// Creates a new shelter
        /// </summary>
        /// <param name="dogShelter">New shelter</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [HttpPost(Name = "AddDogShelter")]
        public IActionResult Post([FromBody] DogShelter dogShelter)
        {
            var validationError = ValidateDogShelter(dogShelter);

            if (validationError != null)
            {
                return validationError;
            }

            dogShelter.Id = DataSet.maxDogShelterId + 1;
            DataSet.maxDogShelterId++;

            _dogShelters.Add(dogShelter);

            return CreatedAtAction(nameof(Get), new { id = dogShelter.Id }, dogShelter);
        }

        /// <summary>
        /// Changes a shelter by id
        /// </summary>
        /// <param name="id">The id of shelter that has to be changed</param>
        /// <param name="dogShelter">Changed shelter</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [HttpPut("{id:int:min(1)}", Name = "ChangeDogShelter")]
        public IActionResult Put(int id, DogShelter dogShelter)
        {
            var alreadyCreatedDogShelter = _dogShelters.FirstOrDefault(dogShelter => dogShelter.Id == id);

            if (alreadyCreatedDogShelter == null)
            {
                return NotFound();
            }

            var validationError = ValidateDogShelter(dogShelter);

            if (validationError != null)
            {
                return validationError;
            }

            alreadyCreatedDogShelter.Name = dogShelter.Name;
            alreadyCreatedDogShelter.Address = dogShelter.Address;
            alreadyCreatedDogShelter.ContactNumber = dogShelter.ContactNumber;

            return NoContent();
        }

        /// <summary>
        /// Deletes a shelter by id
        /// </summary>
        /// <param name="id">The id of shelter that has to be deleted</param>
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [HttpDelete("{id:int:min(1)}", Name = "DeleteDogShelter")]
        public IActionResult Delete(int id)
        {
            var dogShelter = _dogShelters.FirstOrDefault(dogShelter => dogShelter.Id == id);

            if (dogShelter == null)
            {
                return NotFound();
            }

            _dogShelters.Remove(dogShelter);

            return NoContent();
        }

        private IActionResult ValidateDogShelter(DogShelter dogShelter)
        {
            if (dogShelter == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(dogShelter.Name) || dogShelter.Name == "string")
            {
                return BadRequest("Name is not acceptable.");
            }

            if (string.IsNullOrEmpty(dogShelter.Address) || dogShelter.Address == "string")
            {
                return BadRequest("Address is not acceptable.");
            }

            if (string.IsNullOrEmpty(dogShelter.ContactNumber) || dogShelter.ContactNumber == "string")
            {
                return BadRequest("Contact number is not acceptable.");
            }

            return null;
        }
    }
}
