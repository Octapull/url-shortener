using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/user/urls")]
[Authorize]
public class UserPanelController : ControllerBase
{
    private readonly IPanelService _panelService;
    private readonly IUserService _userService;

    public UserPanelController(IPanelService panelService, IUserService userService)
    {
        _panelService = panelService;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUrls()
    {
        var email = User.FindFirstValue(JwtRegisteredClaimNames.Email);
        var user = await _userService.GetByEmailAsync(email);
        if (user == null) return Unauthorized();

        var urls = await _panelService.GetUrlsForUserAsync(user);
        return Ok(urls);
    }
    
    [HttpDelete("delete/{code}")]
    public async Task<IActionResult> DeleteUrlByCode(string code)
    {
        var email = User.FindFirstValue(JwtRegisteredClaimNames.Email);
        var user = await _userService.GetByEmailAsync(email);

        if (user == null)
            return Unauthorized();
        
        await _panelService.DeleteUrlByCodeAsync(code, user);
        return NoContent();
    }
    
    [HttpPatch("disable/{code}")]
    public async Task<IActionResult> DisableUrl(string code)
    {
        var email = User.FindFirstValue(JwtRegisteredClaimNames.Email);
        var user = await _userService.GetByEmailAsync(email);

        if (user == null)
            return Unauthorized();

        await _panelService.DisableUrlByCodeAsync(code, user);
        return NoContent();
    }
}