using api.Domain.DTOs.Reponses;
using api.Domain.DTOs.Requests;

namespace api.Services;

public interface IUrlShortenerService
{
    Task<UrlShortenResponseDto> Shorten(UrlShortenRequestDto request);
}