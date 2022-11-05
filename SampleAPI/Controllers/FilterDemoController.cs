using Microsoft.AspNetCore.Mvc;
using SampleAPI.Filters;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilterDemoController : ControllerBase
    {
        [SampleAsyncActionFilter]
        [HttpGet]
        public void Get()
        {
            Response.WriteAsync("Action in! \r\n");
        }
    }
}