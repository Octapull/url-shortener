using System.Security.Claims;
using api.Domain.DTOs.Requests;
using api.Services;
using api.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api")]
public class UrlShortenerController : ControllerBase
{
    private readonly IUrlShortenerService _urlShortenerService;

    public UrlShortenerController(IUrlShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }

    [HttpPost("shorten")]
    public async Task<IActionResult> Shorten([FromBody] UrlShortenRequestDto request)
    {
        var response = await _urlShortenerService.Shorten(request);
        return Ok(response);
    }
    
    
}