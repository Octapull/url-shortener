namespace api.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    
    public string? Email { get; set; }
    public string? Name { get; set; }

    public string Provider { get; set; } = string.Empty;
    public string ProviderId { get; set; } = string.Empty;
    
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public bool IsAdmin { get; set; } = false;
    
    public ICollection<ShortenedUrl> ShortenedUrls { get; set; } = new List<ShortenedUrl>();
}