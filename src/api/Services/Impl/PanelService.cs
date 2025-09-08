using api.Configuration.Enums;
using api.Domain.DTOs.Reponses;
using api.Domain.Entities;
using api.Repositories;

namespace api.Services.Impl;

public class PanelService : IPanelService
{
    private readonly IShortUrlRepository _urlRepository;

    public PanelService(IShortUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }
    
    public async Task<IEnumerable<UrlPanelDto>> GetUrlsForUserAsync(User user)
    {
        var urls = await _urlRepository.GetByUserIdAsync(user.Id);
        return urls.Select(u => new UrlPanelDto
        {
            Code = u.Code,
            LongUrl = u.LongUrl,
            CreatedAt = u.CreatedAt,
            ClickCount = u.ClickCount, 
            Status = u.Status
        });
    }

    public async Task<IEnumerable<UrlPanelDto>> GetUrlsForAdminAsync()
    {
        var urls = await _urlRepository.GetAllAsync();
        return urls.Select(u => new UrlPanelDto
        {
            Code = u.Code,
            LongUrl = u.LongUrl,
            CreatedAt = u.CreatedAt,
            ClickCount = u.ClickCount, 
            Status = u.Status
        });
    }

    public async Task DeleteUrlByCodeAsync(string code, User? user)
    {
        var url = await _urlRepository.GetByCodeAsync(code);
        if (url == null)
            throw new Exception("URL not found");
        
        if (user == null)
            throw new UnauthorizedAccessException("User must be authenticated.");
        
        if (!user.IsAdmin && url.UserId != user.Id)
            throw new UnauthorizedAccessException("You are not allowed.");

        _urlRepository.Delete(url);
        await _urlRepository.SaveChangesAsync(); 
    }

    public async Task DisableUrlByCodeAsync(string code, User? user)
    {
        var url = await _urlRepository.GetByCodeAsync(code);
        if (url == null)
            throw new Exception("URL not found");
        
        if (user == null)
            throw new UnauthorizedAccessException("User must be authenticated.");

        if (!user.IsAdmin && url.UserId != user.Id)
            throw new UnauthorizedAccessException("You are not allowed.");

        url.Status = UrlStatus.Inactive;
        await _urlRepository.UpdateAsync(url);
        await _urlRepository.SaveChangesAsync();
    }
}