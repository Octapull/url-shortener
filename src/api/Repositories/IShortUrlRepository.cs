using api.Domain.Entities;

namespace api.Repositories;

public interface IShortUrlRepository
{
    Task AddAsync(ShortenedUrl entity);
    Task<bool> IsCodeExistsAsync(string code);
    public Task<string?> GetLongUrlByCodeAsync(string code);
}