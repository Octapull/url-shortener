using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("auth")]
public class SsoLoginController : ControllerBase
{
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public SsoLoginController(IGoogleAuthService googleAuthService, IJwtService jwtService, IUserService userService)
    {
        _googleAuthService = googleAuthService;
        _jwtService = jwtService;
        _userService = userService;
    }

    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = _googleAuthService.GenerateGoogleLoginUrl();
        return Redirect(redirectUrl);
    }
    
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback([FromQuery] string code)
    {
        var googleUser = await _googleAuthService.GetUserInfoAsync(code);
        if (googleUser == null)
            return BadRequest("Google login failed");

        var user = await _userService.FindOrCreateAsync(googleUser);
        var token = _jwtService.GenerateToken(user);

        return Ok(new
        {
            Token = token,
            User = new
            {
                user.Name,
                user.Email
            }
        });
    }
    
}