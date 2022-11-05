using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExceptionDemoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => throw new Exception("Sample Expection");
    }
}