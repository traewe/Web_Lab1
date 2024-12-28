using AutoMapper;
using Web_Lab2.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Lab2.Dtos.DogShelter;
using Web_Lab2.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Web_Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogShelterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDogShelterRepository _repository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<DogShelterController> _logger;

        public DogShelterController(IMapper mapper, IDogShelterRepository repository, IMemoryCache memoryCache, ILogger<DogShelterController> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<DogShelterOutputDto>> Get()
        {
            _logger.LogDebug("Get request for all dog shelters");

            return _mapper.ProjectTo<DogShelterOutputDto>(_repository.GetAll()).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<DogShelterOutputDto?> Get(int id)
        {
            _logger.LogDebug("Get request for the dog shelter with id {id}", id);

            var dog = await _repository.FindAsync(id);

            return _mapper.Map<DogShelterOutputDto>(dog);
        }

        [HttpGet("{name}/{address}")]
        public async Task<DogShelterOutputDto?> Get(string name, string address)
        {
            _logger.LogDebug("Get request for the dog shelter with name {name} and address {address}", name, address);

            var dog = await _repository.FindByNameAndAddressAsync(name, address);

            return _mapper.Map<DogShelterOutputDto>(dog);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DogShelterCreateDto dto)
        {
            _logger.LogDebug("Post request for the dog shelter");

            await _repository.AddAsync(_mapper.Map<DogShelter>(dto));

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DogShelterCreateDto dto)
        {
            _logger.LogDebug("Put request for the dog shelter with {id}", id);

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
            _logger.LogDebug("Delete request for the dog shelter with {id}", id);

            var dogShelter = await _repository.FindAsync(id);

            if (dogShelter == null)
            {
                return NotFound($"DogShelter with ID {id} was not found.");
            }

            await _repository.DeleteAsync(dogShelter);

            return NoContent();
        }
    }
}
