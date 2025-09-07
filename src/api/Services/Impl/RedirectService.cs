using api.Configuration.Enums;
using api.Repositories;

namespace api.Services.Impl;

public class RedirectService : IRedirectService
{
    private readonly IShortUrlRepository _shortUrlRepository;

    public RedirectService(IShortUrlRepository shortUrlRepository)
    {
        _shortUrlRepository = shortUrlRepository;
    }

    public async Task<string?> RedirectAsync(string code)
    {
        var entity = await _shortUrlRepository.GetByCodeAsync(code);
        if (entity == null || entity.Status != UrlStatus.Active)
            return null;
        
        entity.ClickCount += 1;
        entity.LastAccessedAt = DateTime.UtcNow;

        await _shortUrlRepository.UpdateAsync(entity);
        return entity.LongUrl;
    }
}