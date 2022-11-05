using Microsoft.AspNetCore.Mvc;
using SampleAPI.Interface;
using SampleAPI.Services;

namespace MP22152.Controllers;

[ApiController]
[Route("[controller]")]
public class DIDemoController : ControllerBase
{
    public readonly SampleService _sampleService;
    private readonly ITransient _transient;
    private readonly IScoped _scoped;
    private readonly ISingleton _singleton;

    public DIDemoController(SampleService sampleService, ITransient transient, IScoped scoped, ISingleton singleton)
    {
        _sampleService = sampleService;
        _transient = transient;
        _scoped = scoped;
        _singleton = singleton;
    }

    [HttpGet]
    public ActionResult<IDictionary<string, string>> Get()
    {
        var serviceHashCode = _sampleService.GetSampleHashCode();
        var controllerHashCode = $"Transient: {_transient.GetHashCode()}, "
            + $"Scoped: {_scoped.GetHashCode()}, "
            + $"Singleton: {_singleton.GetHashCode()}";
        return new Dictionary<string, string>
            {
                { "Service", serviceHashCode },
                { "Controller", controllerHashCode }
            };
    }
}
