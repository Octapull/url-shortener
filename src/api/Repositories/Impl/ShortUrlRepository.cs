using api.Data;
using api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Impl;

public class ShortUrlRepository : IShortUrlRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ShortUrlRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(ShortenedUrl entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsCodeExistsAsync(string code)
    {
        return await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code);
    }

    public async Task<string?> GetLongUrlByCodeAsync(string code)
    {
        return await _dbContext.ShortenedUrls
            .Where(s => s.Code == code)
            .Select(s => s.LongUrl)
            .FirstOrDefaultAsync();
    }
}