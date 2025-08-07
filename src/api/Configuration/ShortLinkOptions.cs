namespace api.Configuration;

public class ShortLinkOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public int DefaultLength { get; set; }
    public string AllowedCharacters { get; set; } = string.Empty;
}