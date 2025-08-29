namespace api.Domain.Entities;

public class UrlClickStat
{
    public Guid Id { get; set; }
    public Guid ShortenedUrlId { get; set; }
    public DateTime ClickedOn { get; set; } = DateTime.UtcNow;
    public string? Referer { get; set; }
}