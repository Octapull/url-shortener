using api.Domain.Entities;

namespace api.Repositories;

public interface IClickStatRepository
{
    Task AddAsync(UrlClickStat clickStat);
    Task SaveChangesAsync();
}