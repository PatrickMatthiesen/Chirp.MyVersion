namespace Chirp.Core.DTOs;

public record AuthorDetailsDTO {
    public string DisplayName { get; init; }
    public string Email { get; init; }
    public string UserName { get; init; }
    public string ExternalProvider { get; init; }
    public string ExternalId { get; init; }
}
