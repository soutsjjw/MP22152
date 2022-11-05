using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggerDemoController : ControllerBase
    {
        private readonly ILogger<LoggerDemoController> _logger;

        public LoggerDemoController(ILogger<LoggerDemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogWarning("LogginSample in");
            return "Logging API";
        }
    }
}