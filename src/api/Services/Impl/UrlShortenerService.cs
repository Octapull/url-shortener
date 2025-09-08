using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Configuration;
using api.Configuration.Enums;
using api.Domain.DTOs.Reponses;
using api.Domain.DTOs.Requests;
using api.Domain.Entities;
using api.Repositories;
using Microsoft.Extensions.Options;

namespace api.Services.Impl;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly IShortUrlRepository _repository;
    private readonly ShortLinkOptions _shortLinkOptions;
    private readonly ICodeGenerator _codeGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRecaptchaService _recaptchaService;
    
    public UrlShortenerService(IShortUrlRepository repository,
        IOptions<ShortLinkOptions> options,
        ICodeGenerator codeGenerator,
        IHttpContextAccessor httpContextAccessor,
        IRecaptchaService recaptchaService)
    {
        _repository = repository;
        _shortLinkOptions = options.Value;
        _codeGenerator = codeGenerator;
        _httpContextAccessor = httpContextAccessor;
        _recaptchaService = recaptchaService;

    }
    public async Task<UrlShortenResponseDto> Shorten(UrlShortenRequestDto request)
    {
        Guid? userId = null;
        var httpUser = _httpContextAccessor.HttpContext?.User;

        if (httpUser?.Identity?.IsAuthenticated == true)
        {
            var claim = httpUser.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier || c.Type == JwtRegisteredClaimNames.Sub);

            if (claim != null && Guid.TryParse(claim.Value, out var parsedGuid))
            {
                userId = parsedGuid;
            }
        }
        
        if (userId == null)
        {
            if (string.IsNullOrEmpty(request.RecaptchaToken))
                throw new UnauthorizedAccessException("Recaptcha token is required for anonymous users.");

            var isValid = await _recaptchaService.VerifyTokenAsync(request.RecaptchaToken);
            if (!isValid)
                throw new UnauthorizedAccessException("Recaptcha verification failed.");
        }
        
        string code;
        do
        {
            code = _codeGenerator.GenerateCode();
        } 
        while (await _repository.IsCodeExistsAsync(code));

        var shortUrl = $"{_shortLinkOptions.BaseUrl}{code}";
        
        var shortUrlEntity = new ShortenedUrl
        {
            Id = Guid.NewGuid(),
            LongUrl = request.LongUrl,
            Code = code,
            CreatedAt = DateTime.UtcNow,
            UserId = userId, 
            Status = UrlStatus.Active
        };

        await _repository.AddAsync(shortUrlEntity);
        await _repository.SaveChangesAsync();

        return new UrlShortenResponseDto
        {
            ShortUrl = shortUrl,
            Code = code
        };
    }

}