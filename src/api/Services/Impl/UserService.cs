using api.Domain.DTOs.Reponses;
using api.Domain.Entities;
using api.Repositories;

namespace api.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> FindOrCreateAsync(GoogleUserInfoResponse googleUser)
    {
        var user = await _userRepository.GetByProviderIdAsync("Google", googleUser.Id);

        if (user != null)
        {
            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }
        
        user = new User
        {
            Id = Guid.NewGuid(),
            Name = googleUser.Name,
            Email = googleUser.Email,
            Provider = "Google",
            ProviderId = googleUser.Id,
            CreatedOn = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return user;
    }
}