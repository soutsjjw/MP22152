using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [EnableCors("CustomPolicy2")]
    [ApiController]
    [Route("[controller]")]
    public class CorsDemoController : ControllerBase
    {
        [HttpGet("")]
        public string GetModels()
        {
            return "";
        }

        [DisableCors]
        [HttpGet("{id}")]
        public string GetModelById(int id)
        {
            return "";
        }
    }
}