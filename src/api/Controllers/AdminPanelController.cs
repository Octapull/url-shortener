using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/admin/urls")]
[Authorize(Roles = "Admin")]
public class AdminPanelController : ControllerBase
{
    private readonly IPanelService _panelService;

    public AdminPanelController(IPanelService panelService)
    {
        _panelService = panelService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await _panelService.GetUrlsForAdminAsync();
        return Ok(urls);
    }
    
    [HttpDelete("delete/{code}")]
    public async Task<IActionResult> DeleteUrl(string code)
    {
        await _panelService.DeleteUrlByCodeAsync(code, null); 
        return NoContent();
    }
    
    [HttpPatch("disable/{code}")]
    public async Task<IActionResult> DisableUrl(string code)
    {
        await _panelService.DisableUrlByCodeAsync(code, null); 
        return NoContent();
    }
}