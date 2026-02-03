using System.Data.Common;
using Chirp.Core.DTO;
using MiniTwit.Infrastructure.Data;
using MiniTwit.Infrastructure.Entities;
using MiniTwit.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MiniTwit.Infrastructure.Tests.Repositories;

public class CheepRepositoryTest : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<ChirpDBContext> _options;

    public CheepRepositoryTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(_connection)
            .Options;
    }

    ChirpDBContext CreateDbContext() => new ChirpDBContext(_options);

    public void Dispose() => _connection.Dispose();

    [Fact]
    public async Task CreateCheepTest()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new CheepRepository(context);
        var author = new Author { Name = "Test1", Id = "1", Email = "test1@itu.dk" };

        context.Authors.AddRange(author);
        context.SaveChanges();

        var newCheep = new CheepDTO
        {
            Id = 1,
            AuthorId = "1",
            Text = "I chirped",
            CreatedAt = new DateTime(2025, 10, 8),
        };
        //Act
        await repository.CreateCheep(newCheep);

        //Assert
        var cheeps = await repository.GetAllCheeps();
        var numberOfCheeps = cheeps.Count();
        Assert.Equal(1, numberOfCheeps);

        // Clean up
        Dispose();
    }

    [Fact]
    public async Task GetAllCheepsTest()
    {
        //Arrange
        using var context = CreateDbContext();

        context.Database.EnsureCreated();
        var repository = new CheepRepository(context);

        //making authors to relate to the cheeps
        var author1 = new Author { Name = "Test1", Email = "test@itu.dk" };
        var author2 = new Author { Name = "Test2", Email = "test2@itu.dk" };

        context.Authors.AddRange(author1, author2);
        //making cheeps
        context.Cheeps.AddRange(
            new Cheep { Author = author1, Text = "hi", Date = new DateTime(2025, 10, 2),LikedBy = new List<string>() },
            new Cheep { Author = author2, Text = "hello", Date = new DateTime(2025, 10, 3),LikedBy = new List<string>() },
            new Cheep { Author = author1, Text = "hey", Date = new DateTime(2025, 10, 1),LikedBy = new List<string>() }
        );
        context.SaveChanges();

        //Act
        var allCheeps = await repository.GetAllCheeps();

        //Assert
        Assert.Equal(3, allCheeps.Count);
        Assert.Contains(allCheeps, c => c.Text == "hi" && c.AuthorName == "Test1");
        Assert.Contains(allCheeps, c => c.Text == "hello" && c.AuthorName == "Test2");

        //Assert cheeps are in correct order (newest first)
        Assert.True(allCheeps[0].CreatedAt > allCheeps[1].CreatedAt);
        Assert.True(allCheeps[1].CreatedAt > allCheeps[2].CreatedAt);
        Assert.True(allCheeps[0].CreatedAt > allCheeps[2].CreatedAt);

        // Clean up
        Dispose();
    }

    [Fact]
    public async Task GetAllCheepsPaginationTest()
    {
        //Arrange
        using var context = CreateDbContext();

        context.Database.EnsureCreated();
        var repository = new CheepRepository(context);

        // Create 40 cheeps
        var author = new Author { Name = "Test", Email = "test@itu.dk" };
        context.Authors.AddRange(author);
        for (int i = 0; i < 40; i++) 
        {
            context.Cheeps.AddRange(new Cheep { Author = author, Text = "wee", Date = new DateTime(2025, 10, 2),LikedBy = new List<string>() });
        }
        context.SaveChanges();
        
        // Act, assert
        var firstPage = await repository.GetAllCheeps(1);
        Assert.Equal(32, firstPage.Count());
        var secondPage = await repository.GetAllCheeps(2);
        Assert.Equal(8, secondPage.Count());
        
        // Clean up
        Dispose();
    }
    
    [Fact]
    public async Task ReadCheepsByTest()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new CheepRepository(context);

        var author1 = new Author { Name = "Test1", Email = "test1@itu.dk" };
        var author2 = new Author { Name = "Test2", Email = "test2@itu.dk" };
        context.Authors.AddRange(author1, author2);

        //note: setup for these 3 test cheeps was suggested by ChatGPT
        context.Cheeps.AddRange(
            new Cheep { Author = author1, Text = "a1", Date = new DateTime(2025, 10, 10),LikedBy = new List<string>() },
            new Cheep { Author = author1, Text = "a2", Date = new DateTime(2025, 10, 11),LikedBy = new List<string>() },
            new Cheep { Author = author2, Text = "b1", Date = new DateTime(2025, 10, 12),LikedBy = new List<string>() }
        );
        context.SaveChanges();

        //Act
        var author1Cheeps = await repository.ReadCheepsBy("Test1");

        //Assert
        Assert.Equal(2, author1Cheeps.Count);
        Assert.All(author1Cheeps, c => Assert.Equal("Test1", c.AuthorName));

        //Assert cheeps are in correct order (newest first)
        Assert.True(author1Cheeps[0].CreatedAt > author1Cheeps[1].CreatedAt);

        // Clean up
        Dispose();
    }

    [Fact]
    public async Task ReadCheepsByPaginationTest()
    {
        //Arrange
        using var context = CreateDbContext();

        context.Database.EnsureCreated();
        var repository = new CheepRepository(context);

        // Create 50 cheeps
        var author = new Author { Name = "Test", Email = "test@itu.dk" };
        context.Authors.AddRange(author);
        for (int i = 0; i < 50; i++) 
        {
            context.Cheeps.AddRange(new Cheep { Author = author, Text = "wee", Date = new DateTime(2025, 10, 2),LikedBy = new List<string>() });
        }
        context.SaveChanges();
        
        // Act, assert
        var firstPage = await repository.ReadCheepsBy(author.Name, 1);
        Assert.Equal(32, firstPage.Count());
        var secondPage = await repository.ReadCheepsBy(author.Name, 2);
        Assert.Equal(18, secondPage.Count());
        
        // Clean up
        Dispose();
    }
    
    [Fact]
    public async Task ReadCheepsBySelfAndOthersTest()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new CheepRepository(context);

        var author1 = new Author { Name = "Test1", Email = "test1@itu.dk" };
        var author2 = new Author { Name = "Test2", Email = "test2@itu.dk" };
        context.Authors.AddRange(author1, author2);

        //note: setup for these 3 test cheeps was suggested by ChatGPT
        context.Cheeps.AddRange(
            new Cheep { Author = author1, Text = "a1", Date = new DateTime(2025, 10, 10) },
            new Cheep { Author = author1, Text = "a2", Date = new DateTime(2025, 10, 11) },
            new Cheep { Author = author2, Text = "b1", Date = new DateTime(2025, 10, 12) }
        );
        context.SaveChanges();

        var authors = new List<string>();
        authors.Add(author1.Name);
        authors.Add(author2.Name);
        
        // Act
        var cheeps = await repository.ReadCheepsBySelfAndOthers(authors);
        
        // Assert
        Assert.Equal(3, cheeps.Count);
        
        Assert.Contains(cheeps, c => c.Text == "a1" && c.AuthorId == "Test1");
        Assert.Contains(cheeps, c => c.Text == "a2" && c.AuthorId == "Test1");
        Assert.Contains(cheeps, c => c.Text == "b1" && c.AuthorId == "Test2");

        //Assert cheeps are in correct order (newest first)
        Assert.True(cheeps[0].CreatedAt > cheeps[1].CreatedAt);
        Assert.True(cheeps[1].CreatedAt > cheeps[2].CreatedAt);
        Assert.True(cheeps[0].CreatedAt > cheeps[2].CreatedAt);

        // Clean up
        Dispose();
    }
    
    [Fact]
    public async Task UpdateCheepTest()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new CheepRepository(context);

        var author1 = new Author { Name = "Test1", Id = "1", Email = "test1@itu.dk" };
        var author2 = new Author { Name = "Test2", Id = "2", Email = "test2@itu.dk" };
        context.Authors.AddRange(author1, author2);

        var cheep = new Cheep { Author = author1, Text = "old text", Date = new DateTime(2025, 10, 10),LikedBy = new List<string>()};
        context.Cheeps.Add(cheep);
        context.SaveChanges();

        int cheepId = cheep.CheepId;

        // Act
        var dto = new CheepDTO
        {
            Id = cheepId,
            Text = "altered text",
            CreatedAt = new DateTime(2025, 10, 11),
            AuthorId = "1"
        };

        await repository.UpdateCheep(dto);

        // Assert a change has happened
        Assert.True(context.Cheeps.Any(c => c.Text == "altered text"));
        // Assert new time exists
        Assert.True(context.Cheeps.Any(c => c.Date == new DateTime(2025, 10, 11)));
        // Assert old text is gone
        Assert.False(context.Cheeps.Any(c => c.Text == "old text"));

        // Clean up
        Dispose();
    }

    [Fact]
    public async Task GetCheepsTest()
    {
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new CheepRepository(context);
        var author = new Author { Id = "1", Name = "test", Email = "test@test"};
        context.Authors.AddRange(author);

        int cheepId = 1;
        var cheep = new Cheep {CheepId = cheepId, Author = author, Text = "old text", Date = new DateTime(2025, 10, 10),LikedBy = new List<string>()};
        context.Cheeps.Add(cheep);
        context.SaveChanges();
        
        var savedCheep = await repository.GetCheep(cheepId);
        
        Assert.True(cheep.Text == "old text");
        Assert.True(cheep.Author.Email == "test@test");
        Assert.True(cheep.Date == new DateTime(2025, 10, 10));
        Assert.True(cheep.CheepId == cheepId);
    }
}