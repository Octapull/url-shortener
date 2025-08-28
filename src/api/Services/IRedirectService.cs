namespace api.Services;

public interface IRedirectService
{
    public Task<string?> RedirectAsync(string code);
}