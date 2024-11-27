using AutoMapper;
using Web_Lab2.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web_Lab2.Dtos.DogShelter;
using Web_Lab2.Entities;
using Web_Lab2.Dtos.Dog;

namespace Web_Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogShelterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDogShelterRepository _repository;

        public DogShelterController(IMapper mapper, IDogShelterRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public Task<List<DogShelterOutputDto>> Get() => _mapper.ProjectTo<DogShelterOutputDto>(_repository.GetAll()).ToListAsync();

        [HttpGet("{id}")]
        public async Task<DogShelterOutputDto?> Get(int id)
        {
            var dog = await _repository.FindAsync(id);

            return _mapper.Map<DogShelterOutputDto>(dog);
        }

        [HttpGet("{name}/{address}")]
        public async Task<DogShelterOutputDto?> Get(string name, string address)
        {
            var dog = await _repository.FindByNameAndAddressAsync(name, address);

            return _mapper.Map<DogShelterOutputDto>(dog);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DogShelterCreateDto dto)
        {
            await _repository.AddAsync(_mapper.Map<DogShelter>(dto));

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DogShelterCreateDto dto)
        {
            var dogShelter = await _repository.FindAsync(id);

            if (dogShelter == null)
            {
                return NotFound($"DogShelter with ID {id} was not found.");
            }

            _mapper.Map(dto, dogShelter);

            await _repository.UpdateAsync(dogShelter);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dogShelter = await _repository.FindAsync(id);

            if (dogShelter == null)
            {
                return NotFound($"DogShelter with ID {id} was not found.");
            }

            await _repository.DeleteAsync(dogShelter);

            return NoContent();
        }


        /*private IActionResult ValidateDog(Dog dog)
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
        }*/
    }
}
