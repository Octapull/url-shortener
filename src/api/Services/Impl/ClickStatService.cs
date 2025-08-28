using api.Domain.Entities;
using api.Repositories;

namespace api.Services.Impl;

public class ClickStatService : IClickStatService
{
    private readonly IClickStatRepository _clickStatRepository;
    private readonly IShortUrlRepository _shortUrlRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClickStatService
    (
        IClickStatRepository clickStatRepository,
        IShortUrlRepository shortUrlRepository,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _clickStatRepository = clickStatRepository;
        _shortUrlRepository = shortUrlRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task RecordClickAsync(string code)
    {
        var shortUrl = await _shortUrlRepository.GetByCodeAsync(code);
        if (shortUrl == null) return;

        var context = _httpContextAccessor.HttpContext;
        var referer = context?.Request.GetTypedHeaders().Referer?.ToString();

        var stat = new UrlClickStat
        {
            Id = Guid.NewGuid(),
            ShortenedUrlId = shortUrl.Id,
            ClickedOn = DateTime.UtcNow,
            Referer = referer
        };
        
        await _clickStatRepository.AddAsync(stat);
        await _clickStatRepository.SaveChangesAsync();
    }
}