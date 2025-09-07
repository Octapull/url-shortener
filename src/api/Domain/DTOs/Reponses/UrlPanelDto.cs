using System.Text.Json.Serialization;
using api.Configuration.Enums;

namespace api.Domain.DTOs.Reponses;

public class UrlPanelDto
{
    public string Code { get; set; } = string.Empty;
    public string LongUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public long ClickCount { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UrlStatus Status { get; set; }
}