using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("")]
public class RedirectController : ControllerBase
{
    private readonly IRedirectService _redirectService;
    private readonly IClickStatService _clickStatService;

    public RedirectController
    (
        IRedirectService redirectService,
        IClickStatService clickStatService
    )
    {
        _redirectService = redirectService;
        _clickStatService = clickStatService;
    }
    
    [HttpGet("{code}")]
    public async Task<IActionResult> RedirectToLongUrl(string code)
    {
        var longUrl = await _redirectService.RedirectAsync(code);
        
        if (string.IsNullOrEmpty(longUrl))
            return NotFound("Short link not found.");
        
        await _clickStatService.RecordClickAsync(code);
        return Redirect(longUrl);
    }
}