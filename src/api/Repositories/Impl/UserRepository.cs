using api.Data;
using api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Impl;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> GetByProviderIdAsync(string provider, string providerId)
    {
        return await _dbContext.Users
            .SingleOrDefaultAsync(u => u.Provider == provider && u.ProviderId == providerId);
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();

    }
}