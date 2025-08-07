using api.Repositories;
using api.Repositories.Impl;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Services.Impl;

public class RedirectService : IRedirectService
{
    private readonly IShortUrlRepository _shortUrlRepository;

    public RedirectService(IShortUrlRepository shortUrlRepository)
    {
        _shortUrlRepository = shortUrlRepository;
    }

    public async Task<string?> GetLongUrlByCodeAsync(string code)
    {
        var longUrl = await _shortUrlRepository.GetLongUrlByCodeAsync(code);
        return longUrl;
    }
}