namespace MiniTwit.Core.DTO;

public class CheepDTO
{
    public int Id { get; set; }
    public required string AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public required string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> LikedBy { get; set; } = new List<string>();
}