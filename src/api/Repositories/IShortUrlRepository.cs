using api.Domain.Entities;

namespace api.Repositories;

public interface IShortUrlRepository
{
    Task AddAsync(ShortenedUrl entity);
    Task UpdateAsync(ShortenedUrl entity);
    Task<bool> IsCodeExistsAsync(string code);
    Task<ShortenedUrl?> GetByCodeAsync(string code);
    Task SaveChangesAsync();
    void Delete(ShortenedUrl url);
    Task<IEnumerable<ShortenedUrl>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<ShortenedUrl>> GetAllAsync();
    
}