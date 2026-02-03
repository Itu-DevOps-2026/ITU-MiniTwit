using System.ComponentModel.DataAnnotations;

using MiniTwit.Infrastructure.Entities;

namespace MiniTwit.Infrastructure.Tests.DataModel;

public class CheepTest
{
    [Fact]
    public void CheepStoresAndReturnsAssignedValues()
    {
        var author = new Author
        {
            Name = "Tester",
            Id = "10",
            Email = "test@itu.dk"
        };
        var date = new DateTime(2025, 1, 1);
        var cheep = new Cheep
        {
            CheepId = 1,
            Text = "This is a test",
            Date = date,
            Author = author,
            LikedBy = []
        };
        
        Assert.Equal(1, cheep.CheepId);
        Assert.Equal(author, cheep.Author);
        Assert.Equal("This is a test", cheep.Text);
        Assert.Equal(new DateTime(2025, 1, 1), cheep.Date);
    }

    [Theory]
   [InlineData("test1","Hello world! This is a short cheep under 160 characters.", true)]
   [InlineData("test2",
       "This cheep has exactly one hundred sixty characters. It is carefully crafted so that the total number of characters in this string adds up to 159",
        true)]
    [InlineData("test3",
        "This cheep is way too long. It exceeds one hundred sixty characters easily. Its purpose is to test how the system handles cheeps that are over the maximum allowed length limit.",
        false)]
    public void constraintLengthOnCheepTest(string name, string input, bool expected)
    {
       
        var author = new Author()
        {
            Name = name,
            Email = "text@test.dk"
        };

        var cheep = new Cheep
        {
            Author = author,
            Date = new DateTime(2025, 1, 1),
            Text = input,
            LikedBy = []
        };

        var validationContext = new ValidationContext(cheep);
        var validationResults = new List<ValidationResult>();
        
        bool result = Validator.TryValidateObject(cheep, validationContext, validationResults, true);
        
        Assert.Equal(result, expected);
    }
    
}