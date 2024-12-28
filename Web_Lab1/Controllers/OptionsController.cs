using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web_Lab2.Options;

namespace Web_Lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionsMonitor<ProgramAuthor> _monitor;
        private readonly ILogger<OptionsController> _logger;

        public OptionsController(IOptionsMonitor<ProgramAuthor> monitor, ILogger<OptionsController> logger)
        {
            _monitor = monitor;
            _logger = logger;
        }

        [HttpGet]
        public object Get()
        {
            _logger.LogDebug("Get request for options");

            return new { Monitor = _monitor.CurrentValue };
        }
    }
}
