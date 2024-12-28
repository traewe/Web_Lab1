using AutoMapper;
using Web_Lab2.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Web_Lab2.Entities;

namespace Web_Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CityController> _logger;

        public CityController(IMemoryCache memoryCache, ILogger<CityController> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet("memory")]
        public IEnumerable<City> GetMemory()
        {
            _logger.LogDebug("Memory request for stray dogs");

            return _memoryCache.GetOrCreate(nameof(GetMemory), cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

                var cities = City.GenerateRandom().ToList();

                return cities;
            })!;
        }
    }
}
