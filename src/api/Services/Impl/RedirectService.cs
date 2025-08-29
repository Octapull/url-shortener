using api.Repositories;

namespace api.Services.Impl;

public class RedirectService : IRedirectService
{
    private readonly IShortUrlRepository _shortUrlRepository;
    private readonly IClickStatService _clickStatService;

    public RedirectService(
        IShortUrlRepository shortUrlRepository,
        IClickStatService clickStatService)
    {
        _shortUrlRepository = shortUrlRepository;
        _clickStatService = clickStatService;
    }

    public async Task<string?> RedirectAsync(string code)
    {
        var entity = await _shortUrlRepository.GetByCodeAsync(code);
        if (entity == null)
            return null;
        
        entity.ClickCount += 1;
        entity.LastAccessedAt = DateTime.UtcNow;

        await _shortUrlRepository.UpdateAsync(entity);
        return entity.LongUrl;
    }
}