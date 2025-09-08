using api.Domain.DTOs.Reponses;
using api.Domain.Entities;

namespace api.Services;

public interface IPanelService 
{
    Task<IEnumerable<UrlPanelDto>> GetUrlsForUserAsync(User user);
    Task<IEnumerable<UrlPanelDto>> GetUrlsForAdminAsync();
    Task DeleteUrlByCodeAsync(string code, User user);
    Task DisableUrlByCodeAsync(string code, User user);
}