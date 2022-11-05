using System.ComponentModel.DataAnnotations;
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
        [Required]
        [StringLength(6, ErrorMessage = "名字長度必須介於 {2} 到 {1} 個字", MinimumLength =2)]
        public string Name { get; set; } = default!;

        [Range(5, 50)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email 為必填")]
        [EmailAddress]
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;

        [Compare("Password")]
        public string ConfirmPassword { get; set; } = default!;
    }
}