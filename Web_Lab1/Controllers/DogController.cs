using AutoMapper;
using Web_Lab2.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web_Lab2.Dtos.Dog;
using Web_Lab2.Entities;

namespace Web_Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDogRepository _repository;

        public DogController(IMapper mapper, IDogRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public Task<List<DogOutputDto>> Get() => _mapper.ProjectTo<DogOutputDto>(_repository.GetAll()).ToListAsync();

        [HttpGet("{id}")]
        public async Task<DogOutputDto?> Get(int id)
        {
            var dog = await _repository.FindAsync(id);

            return _mapper.Map<DogOutputDto>(dog);
        }

        [HttpGet("{name}/{breed}")]
        public async Task<DogOutputDto?> Get(string name, string breed)
        {
            var dog = await _repository.FindByNameAndBreedAsync(name, breed);

            return _mapper.Map<DogOutputDto>(dog);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DogCreateDto dto)
        {
            await _repository.AddAsync(_mapper.Map<Dog>(dto));

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DogCreateDto dto)
        {
            var dog = await _repository.FindAsync(id);

            if (dog == null)
            {
                return NotFound($"Dog with ID {id} was not found.");
            }

            _mapper.Map(dto, dog);

            await _repository.UpdateAsync(dog);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dog = await _repository.FindAsync(id);

            if (dog == null)
            {
                return NotFound($"Dog with ID {id} was not found.");
            }

            await _repository.DeleteAsync(dog);

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
