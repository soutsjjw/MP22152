using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers;

public class SecurityController : Controller
{
    private readonly ILogger<LoggerDemoController> _logger;
    private readonly HtmlEncoder _htmlEncoder;
    private readonly JavaScriptEncoder _javaScriptEncoder;
    private readonly UrlEncoder _urlEncoder;
    private readonly IAntiforgery _antiforgery;

    public SecurityController(ILogger<LoggerDemoController> logger, HtmlEncoder htmlEncoder, JavaScriptEncoder javaScriptEncoder, UrlEncoder urlEncoder, IAntiforgery antiforgery)
    {
        _logger = logger;
        _htmlEncoder = htmlEncoder;
        _javaScriptEncoder = javaScriptEncoder;
        _urlEncoder = urlEncoder;
        _antiforgery = antiforgery;
    }

    public IActionResult XssAttack(string searchString)
    {
        var tokens = _antiforgery.GetAndStoreTokens(HttpContext);

        HttpContext.Response.Cookies.Append("security", "SecurityValue",
        new CookieOptions
        {
            HttpOnly = true,
            Path = "/",
            IsEssential = true,
            SameSite = SameSiteMode.Lax
        });

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            ViewData["html"] = _htmlEncoder.Encode(searchString);
            ViewData["js"] = _javaScriptEncoder.Encode(searchString);
            ViewData["url"] = _urlEncoder.Encode(searchString);

            _logger.LogInformation($"html: {ViewData["html"]?.ToString()}");
            _logger.LogInformation($"js: {ViewData["js"]?.ToString()}");
            _logger.LogInformation($"url: {ViewData["url"]?.ToString()}");
        }

        return View((object)searchString);
    }
}
