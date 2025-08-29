using api.Domain.DTOs.Reponses;
using api.Domain.Entities;

namespace api.Services;

public interface IGoogleAuthService
{
    string GenerateGoogleLoginUrl();
    Task<GoogleUserInfoResponse?> GetUserInfoAsync(string code);
}