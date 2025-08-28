using api.Domain.Entities;

namespace api.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}