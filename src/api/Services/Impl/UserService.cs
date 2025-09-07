using api.Configuration;
using api.Domain.DTOs.Reponses;
using api.Domain.Entities;
using api.Repositories;
using Microsoft.Extensions.Options;

namespace api.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly AdminOptions _adminOptions;
    public UserService(IUserRepository userRepository, IOptions<AdminOptions> adminOptions) 
    {
        _userRepository = userRepository;
        _adminOptions = adminOptions.Value;
    }
    public async Task<User> FindOrCreateAsync(GoogleTokenPayload googleUser)
    {
        var user = await _userRepository.GetByEmailAsync(googleUser.Email);
        
        if (user != null)
        {
            user.LastLoginAt = DateTime.UtcNow;
            user.IsAdmin = IsAdminUser(googleUser.Email);
            
            await _userRepository.SaveChangesAsync();
            return user;
        }
        
        user = new User
        {
            Id = Guid.NewGuid(),
            Name = string.IsNullOrWhiteSpace(googleUser.Name) 
                ? googleUser.Email.Split('@')[0] 
                : googleUser.Name,
            Email = googleUser.Email,
            Provider = "Google",
            ProviderId = googleUser.Id,
            CreatedOn = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow,
            IsAdmin = IsAdminUser(googleUser.Email)
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    private bool IsAdminUser(string email)
    {
        return _adminOptions.AllowedAdminEmails.Any(adminEmail =>
            adminEmail.Equals(email, StringComparison.OrdinalIgnoreCase));
    }
    
    
}