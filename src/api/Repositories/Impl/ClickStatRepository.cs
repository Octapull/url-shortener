using api.Data;
using api.Domain.Entities;

namespace api.Repositories.Impl;

public class ClickStatRepository : IClickStatRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ClickStatRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(UrlClickStat clickStat)
    {
        await _dbContext.UrlClickStats.AddAsync(clickStat);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}