using Microsoft.AspNetCore.Mvc;
using Web_Lab1.Data;

namespace Web_Lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogShelterController : ControllerBase
    {
        private readonly List<DogShelter> _dogShelters;
        private readonly ILogger<DogShelterController> _logger;

        public DogShelterController(ILogger<DogShelterController> logger)
        {
            _logger = logger;
            _dogShelters = DataSet.DogShelters;
        }

        [HttpGet(Name = "GetAllDogShelters")]
        public IEnumerable<DogShelter> GetAll()
        {
            return _dogShelters;
        }
    }
}
