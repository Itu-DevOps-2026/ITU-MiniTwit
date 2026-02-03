namespace MiniTwit.Core.DTO;

public class AuthorDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string? Email { get; set; }
    public ICollection<CheepDTO> Cheeps { get; set; } = new List<CheepDTO>();
    public IList<string> Following { get; set; } = new List<string>();

}