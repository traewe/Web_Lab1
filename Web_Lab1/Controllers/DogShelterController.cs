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

        [HttpGet(Name = "GetAllDogShelters")]
        public IEnumerable<DogShelter> GetAll()
        {
            return _dogShelters;
        }

        [HttpGet("{id:int:min(1)}", Name = "GetDogShelterById")]
        public IActionResult Get(int id)
        {
            var shelter = _dogShelters.FirstOrDefault(shelter => shelter.Id == id);

            return shelter != null ? Ok(shelter) : NotFound();
        }

        [HttpPost(Name = "AddDogShelter")]
        public IActionResult Post([FromBody] DogShelter dogShelter)
        {
            if (dogShelter == null)
            {
                return NotFound();
            }

            dogShelter.Id = _dogShelters[_dogShelters.Count - 1].Id + 1;

            _dogShelters.Add(dogShelter);

            return CreatedAtAction(nameof(Get), new { id = dogShelter.Id }, dogShelter);
        }
    }
}
