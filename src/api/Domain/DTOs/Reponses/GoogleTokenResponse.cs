using System.Text.Json.Serialization;

namespace api.Domain.DTOs.Reponses;

public class GoogleTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("id_token")]
    public string? IdToken { get; set; }
}