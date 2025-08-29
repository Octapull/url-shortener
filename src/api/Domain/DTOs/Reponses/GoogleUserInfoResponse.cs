using System.Text.Json.Serialization;

namespace api.Domain.DTOs.Reponses;

public class GoogleUserInfoResponse
{

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

}