
using api.Configuration.Enums;

namespace api.Domain.Entities;

public class ShortenedUrl
{
    public Guid Id { get; set; }
    public string LongUrl { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long ClickCount { get; set; } = 0;
    public DateTime LastAccessedAt { get; set; }
    
    public UrlStatus Status { get; set; } = UrlStatus.Active;
    
    public Guid? UserId { get; set; }
    public  User? User { get; set; } 
    
}