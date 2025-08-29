using api.Configuration;
using api.Domain.DTOs.Reponses;
using Microsoft.Extensions.Options;

namespace api.Services.Impl;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly GoogleKeysOptions _googleOptions;
    private readonly HttpClient _httpClient;

    public GoogleAuthService(IOptions<GoogleKeysOptions> googleOptions, HttpClient httpClient)
    {
        _googleOptions = googleOptions.Value;
        _httpClient = httpClient;
    }
    
    public string GenerateGoogleLoginUrl()
    {
        var scope = "openid profile email";
        
        return $"https://accounts.google.com/o/oauth2/v2/auth" +
               $"?client_id={_googleOptions.ClientId}" +
               $"&redirect_uri={_googleOptions.RedirectUri}" +
               $"&response_type=code" +
               $"&scope={scope}";
    }
    
    public async Task<GoogleUserInfoResponse?> GetUserInfoAsync(string code)
    {
        var tokenResponse = await _httpClient.PostAsync(
            "https://oauth2.googleapis.com/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"code", code},
                {"client_id", _googleOptions.ClientId},
                {"client_secret", _googleOptions.ClientSecret},
                {"redirect_uri", _googleOptions.RedirectUri},
                {"grant_type", "authorization_code"}
            }));

        if (!tokenResponse.IsSuccessStatusCode)
            return null;
        
        var tokenResult = await tokenResponse.Content.ReadFromJsonAsync<GoogleTokenResponse>();
        if (tokenResult?.AccessToken == null)
            return null;
        
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);

        var userInfo = await _httpClient.GetFromJsonAsync<GoogleUserInfoResponse>(
            "https://www.googleapis.com/oauth2/v2/userinfo");

        return userInfo;
    }
}