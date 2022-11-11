using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponseCacheController : ControllerBase
{
    private readonly ILogger<ResponseCacheController> _logger;

    public ResponseCacheController(ILogger<ResponseCacheController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
    public Dictionary<string, string> Get()
    {
        return new Dictionary<string, string>
        {
            { "快取在時間", DateTime.Now.ToString() },
        };
    }
}
