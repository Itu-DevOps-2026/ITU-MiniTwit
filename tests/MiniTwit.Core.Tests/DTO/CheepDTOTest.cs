
using MiniTwit.Core.DTO;

namespace MiniTwit.Core.Tests.DTO;

public class CheepDTOTest
{
    [Fact]
    public void CheepDTOStoresAndReturnsAssignedValues()
    {
        CheepDTO cheepDTO = new()
        {
            Id = 1,
            AuthorId = "Tester",
            Text = "This is a test",
            CreatedAt = new DateTime(2025, 1, 1)
        };
        
        Assert.Equal(1, cheepDTO.Id);
        Assert.Equal("Tester", cheepDTO.AuthorId);
        Assert.Equal("This is a test", cheepDTO.Text);
        Assert.Equal(new DateTime(2025, 1, 1), cheepDTO.CreatedAt);
    }
}