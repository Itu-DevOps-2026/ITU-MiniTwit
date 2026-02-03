using MiniTwit.Infrastructure.Entities;

namespace MiniTwit.Infrastructure.Tests.DataModel;

public class AuthorTest
{
    [Fact]
    public void AuthorStoresAndReturnsAssignedValues()
    {
        var author = new Author()
        {
            Id = "1",
            Name = "Test",
            Email = "test@itu.dk"
            
        };

        var cheeps = new List<Cheep>
        {
            new()
            {
                CheepId = 1,
                Text = "This is a test",
                Date = new DateTime(2025, 1, 1),
                Author = new Author() { Name = "Test", Email = "test@itu.dk" },
                LikedBy = []
            }
        };
        author.Cheeps = cheeps;
        
        Assert.Equal("1", author.Id);
        Assert.Equal("Test", author.Name);
        Assert.Equal("test@itu.dk", author.Email);
        Assert.Equal(cheeps, author.Cheeps);
    }
}