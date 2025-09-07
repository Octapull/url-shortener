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
    }

    public async Task UpdateAsync(ShortenedUrl entity)
    { 
        _dbContext.ShortenedUrls.Update(entity);
    }

    public async Task<bool> IsCodeExistsAsync(string code)
    {
        return await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code);
    }
    
    public async Task<ShortenedUrl?> GetByCodeAsync(string code)
    {
        return await _dbContext.ShortenedUrls
            .SingleOrDefaultAsync(s => s.Code == code); 
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Delete(ShortenedUrl url)
    {
        _dbContext.ShortenedUrls.Remove(url);
    }

    public async Task<IEnumerable<ShortenedUrl>> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.ShortenedUrls
            .Where(u => u.UserId == userId)
            .OrderByDescending(u => u.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<ShortenedUrl>> GetAllAsync()
    {
        return await _dbContext.ShortenedUrls
            .OrderByDescending(u => u.CreatedAt)
            .ToListAsync();
    }
}