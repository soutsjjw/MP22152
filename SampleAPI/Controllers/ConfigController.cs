using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _config;

    public ConfigController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet(Name = "GetSample")]
    public string Get()
    {
        var key = _config["Key"];
        var name = _config["Site:Name"];
        var defaultLogLevel = _config["Logging:LogLevel:Default"];
        return $@"Name:{name}
Key:{key}
defaultLogLevel:{defaultLogLevel}";
    }

}
