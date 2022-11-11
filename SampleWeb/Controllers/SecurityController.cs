using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleWeb.Controllers;

public class SecurityController : Controller
{
    private readonly ILogger<SecurityController> _logger;
    private readonly HtmlEncoder _htmlEncoder;
    private readonly JavaScriptEncoder _javaScriptEncoder;
    private readonly UrlEncoder _urlEncoder;

    public SecurityController(ILogger<SecurityController> logger, HtmlEncoder htmlEncoder, JavaScriptEncoder javaScriptEncoder, UrlEncoder urlEncoder)
    {
        _logger = logger;
        _htmlEncoder = htmlEncoder;
        _javaScriptEncoder = javaScriptEncoder;
        _urlEncoder = urlEncoder;
    }

    public IActionResult XssAttack(string searchString)
    {
        HttpContext.Response.Cookies.Append("Security", "SecurityValue",
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
        }

        return View((object)searchString);
    }
}
