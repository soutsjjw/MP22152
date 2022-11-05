using Microsoft.AspNetCore.Mvc;
using SampleAPI.Attributes;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [HiddenAPI]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error")]
        public ActionResult Error() => Problem();
    }
}