using Chirp.Core.DTO;

namespace MiniTwit.Core.Tests.DTO;

public class AuthorDTOTest
{
    [Fact]
    public void AuthorDTOStoresAndReturnsAssignedValues()
    {
        AuthorDTO authorDTO = new()
        {
            Id = "1",
            Name = "Test",
            Email = "test@itu.dk"
        };

        var cheeps = new List<CheepDTO> { new CheepDTO { Id = 1, AuthorId = "Test", Text = "Test1"} };
        authorDTO.Cheeps = cheeps;
        
        Assert.Equal("1", authorDTO.Id);
        Assert.Equal("Test", authorDTO.Name);
        Assert.Equal("test@itu.dk", authorDTO.Email);
        Assert.Equal(cheeps, authorDTO.Cheeps);
    }
}