using AutoMapper;
using Web_Lab2.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Lab2.Dtos.Dog;
using Web_Lab2.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Web_Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDogRepository _repository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<DogController> _logger;

        public DogController(IMapper mapper, IDogRepository repository, IMemoryCache memoryCache, ILogger<DogController> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<DogOutputDto>> Get()
        {
            _logger.LogDebug("Get request for all dogs");

            return _mapper.ProjectTo<DogOutputDto>(_repository.GetAll()).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<DogOutputDto?> Get(int id)
        {
            _logger.LogDebug("Get request for the dog with id {id}", id);

            var dog = await _repository.FindAsync(id);

            return _mapper.Map<DogOutputDto>(dog);
        }

        [HttpGet("{name}/{breed}")]
        public async Task<DogOutputDto?> Get(string name, string breed)
        {
            _logger.LogDebug("Get request for the dog with name {name} and breed {breed}", name, breed);

            var dog = await _repository.FindByNameAndBreedAsync(name, breed);

            return _mapper.Map<DogOutputDto>(dog);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DogCreateDto dto)
        {
            _logger.LogDebug("Post request for the dog");

            await _repository.AddAsync(_mapper.Map<Dog>(dto));

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DogCreateDto dto)
        {
            _logger.LogDebug("Put request for the dog with {id}", id);

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
            _logger.LogDebug("Delete request for the dog with {id}", id);

            var dog = await _repository.FindAsync(id);

            if (dog == null)
            {
                return NotFound($"Dog with ID {id} was not found.");
            }

            await _repository.DeleteAsync(dog);

            return NoContent();
        }
    }
}
