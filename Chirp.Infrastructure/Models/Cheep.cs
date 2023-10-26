namespace Chirp.Infrastructure.Models;

public class Cheep
{
    public int Id { get; set; }
    public required string Message { get; set; }
    public DateTime TimeStamp { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string AuthorId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public required Author Author { get; set; }
}