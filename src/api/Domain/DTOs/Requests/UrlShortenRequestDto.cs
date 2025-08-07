using System.ComponentModel.DataAnnotations;

namespace api.Domain.DTOs.Requests;

public class UrlShortenRequestDto
{
    [Required]
    [Url]
    public string LongUrl { get; set; }
}