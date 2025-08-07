using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class RedirectController : ControllerBase
{
    private readonly IRedirectService _redirectService;

    public RedirectController(IRedirectService redirectService)
    {
        _redirectService = redirectService;
    }
    
    [HttpGet("{code}")]
    public async Task<IActionResult> RedirectToLongUrl(string code)
    {
        var longUrl = await _redirectService.GetLongUrlByCodeAsync(code);
        
        if (string.IsNullOrEmpty(longUrl))
            return NotFound("Short link not found.");

        return Redirect(longUrl);
    }
    
    
}