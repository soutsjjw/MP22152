using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModelBindingDemoController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<string> Get([FromRoute] int id, [FromQuery] int query, [FromHeader] string header1)
        {
            return $"query: {query}, route: {id}, header: {header1}";
        }

        [HttpPost]
        public ActionResult<DemoUser> Post(DemoUser demo)
        {
            return demo;
        }
    }

    public class DemoUser
    {
        public string Name { get; set; } = default!;
        public int Age { get; set; }
    }
}