using Microsoft.AspNetCore.Mvc;
using Web_Lab1.Data;

namespace Web_Lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogController : ControllerBase
    {
        private readonly List<Dog> _dogs;
        private readonly ILogger<DogShelterController> _logger;

        public DogController(ILogger<DogShelterController> logger)
        {
            _logger = logger;
            _dogs = DataSet.Dogs;
        }

        [HttpGet(Name = "GetAllDogs")]
        public IEnumerable<Dog> GetAll()
        {
            return _dogs;
        }
    }
}
