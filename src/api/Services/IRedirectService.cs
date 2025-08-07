namespace api.Services;

public interface IRedirectService
{
    public Task<string?> GetLongUrlByCodeAsync(string code);
}