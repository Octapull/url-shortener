using api.Domain.DTOs.Reponses;
using api.Domain.Entities;

namespace api.Services;

public interface IUserService
{
    Task<User> FindOrCreateAsync(GoogleTokenPayload googleUser);
    Task<User?> GetByEmailAsync(string email);
}