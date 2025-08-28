using api.Configuration;
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
    
    public UrlShortenerService(IShortUrlRepository repository,
        IOptions<ShortLinkOptions> options,
        ICodeGenerator codeGenerator)
    {
        _repository = repository;
        _shortLinkOptions = options.Value;
        _codeGenerator = codeGenerator;

    }
    public async Task<UrlShortenResponseDto> Shorten(UrlShortenRequestDto request)
    {
        string code;
        do
        {
            code = _codeGenerator.GenerateCode();
        } while (await _repository.IsCodeExistsAsync(code));
        
        var shortUrl = $"{_shortLinkOptions.BaseUrl}{code}";
        
        var shortUrlEntity = new ShortenedUrl
        {
            Id = Guid.NewGuid(),
            LongUrl = request.LongUrl,
            ShortUrl = shortUrl,
            Code = code,
            CreatedAt = DateTime.UtcNow,
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