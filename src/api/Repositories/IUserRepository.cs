using api.Domain.Entities;

namespace api.Repositories;

public interface IUserRepository
{
    Task<User?> GetByProviderIdAsync(string provider, string providerId);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task SaveChangesAsync();
}