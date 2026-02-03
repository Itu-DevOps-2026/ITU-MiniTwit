using System.Data.Common;
using Chirp.Core.DTO;
using MiniTwit.Infrastructure.Data;
using MiniTwit.Infrastructure.Entities;
using MiniTwit.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MiniTwit.Infrastructure.Tests.Repositories;

public class AuthorRepositoryTest : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<ChirpDBContext> _options;

    public AuthorRepositoryTest()
    {
        // Make in memory database
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite(_connection)
            .Options;
    }

    ChirpDBContext CreateDbContext() => new (_options);

    public void Dispose() => _connection.Dispose();

    [Fact]
    public async Task CreateAuthorTest()
    {
        // Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new AuthorRepository(context);

        // Act
        var authorDTOTest = new AuthorDTO()
        {
            Id = "1",
            Name = "John Doe",
            Email = "test@itu.dk",
            Cheeps = new List<CheepDTO>()
        };

        await repository.CreateAuthor(authorDTOTest);

        // Assert
        var cheeps = await repository.GetAllAuthors();
        var numberOfCheeps = cheeps.Count();
        Assert.Equal(1, numberOfCheeps);

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task CreateAuthorsWithSameNameTest()
    {
        // Test the method
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new AuthorRepository(context);

        var authorDTOTest = new AuthorDTO()
        {
            Id = "1",
            Name = "John Doe",
            Email = "test@itu.dk",
            Cheeps = new List<CheepDTO>()
        };
        
        var sameNameAuthor = new AuthorDTO()
        {
            Id = "2",
            Name = "John Doe",
            Email = "tester@itu.dk",
            Cheeps = new List<CheepDTO>()
        };

        await repository.CreateAuthor(authorDTOTest);
        await Assert.ThrowsAsync<DbUpdateException>(() => repository.CreateAuthor(sameNameAuthor));

        var authors = await repository.GetAllAuthors();
        Assert.DoesNotContain(authors, a => a.Id == "2");
        
        //Clean up
        Dispose();
    }

    [Fact]
    public async Task GetAllAuthorsTest()
    {
        // Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        context.Authors.AddRange(
            new Author { Id = "1", Cheeps = new List<Cheep>(), Email = "test1@itu.dk", Name = "Test1" },
            new Author { Id = "2", Cheeps = new List<Cheep>(), Email = "test2@itu.dk", Name = "Test2" },
            new Author { Id = "3", Cheeps = new List<Cheep>(), Email = "test3@itu.dk", Name = "Test3" }
        );
        context.SaveChanges();

        // Act
        var repository = new AuthorRepository(context);
        var authors = await repository.GetAllAuthors();

        // Assert
        Assert.Equal(3, authors.Count);
        Assert.Contains(authors, a => a.Id == "1");
        Assert.Contains(authors, a => a.Id == "2");
        Assert.Contains(authors, a => a.Id == "3");

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task UpdateAuthorTest()
    {
        // Arrange
        using var context = CreateDbContext();

        context.Database.EnsureCreated();
        context.Authors.AddRange(
            new Author { Id = "1", Cheeps = new List<Cheep>(), Email = "test1@itu.dk", Name = "Test1" },
            new Author { Id = "2", Cheeps = new List<Cheep>(), Email = "test2@itu.dk", Name = "Test2" }
        );
        context.SaveChanges();


        // Act
        var repository = new AuthorRepository(context);
        var authorDtoTest = new AuthorDTO()
        {
            Id = "1",
            Name = "John Doe",
            Email = "test@itu.dk",
            Cheeps = new List<CheepDTO>()
        };

        await repository.UpdateAuthor(authorDtoTest);

        // Assert
        Assert.True(context.Authors.Any(a => a.Name == "John Doe"));
        var queryToFindUpdatedAuthor = from Author in context.Authors
            where Author.Id == "1"
            select Author;
        var updatedAuthor = queryToFindUpdatedAuthor.Single();
        Assert.NotNull(updatedAuthor);
        Assert.Equal("test@itu.dk", updatedAuthor.Email);
        Assert.True(updatedAuthor.Cheeps.Count == 0);
        Assert.False(context.Authors.Any(a => a.Name == "Test1"));
        Assert.True(context.Authors.Count() == 2);

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task FindByName()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated(); 
        var a1 = new Author { Id = "1", Name = "test1", Email = "test1@itu.dk", Cheeps = new List<Cheep>() };
        var a2 = new Author { Id = "2", Name = "test2",   Email = "test2@itu.dk",   Cheeps = new List<Cheep>() };
        context.Authors.AddRange(a1, a2);
        
        context.Cheeps.AddRange(
            new Cheep { Author = a1, Text = "hello", Date = new DateTime(2025, 10, 10),LikedBy = new List<string>() },
            new Cheep { Author = a1, Text = "world", Date = new DateTime(2025, 10, 11),LikedBy = new List<string>() },
            new Cheep { Author = a2, Text = "cheep", Date = new DateTime(2025, 10, 12),LikedBy = new List<string>() }
        );
        context.SaveChanges();
        
        var repository = new AuthorRepository(context);
        
        //Act
        var dto =  await repository.FindByName("test1");
        
        //Assert
        Assert.NotNull(dto);
        Assert.Equal("test1", dto.Name);
        Assert.Equal("test1@itu.dk", dto.Email);
        Assert.Equal(2, dto.Cheeps.Count);

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task FindByEmail()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated(); 
        var a1 = new Author { Id = "1", Name = "test1", Email = "test1@itu.dk", Cheeps = new List<Cheep>() };
        var a2 = new Author { Id = "2", Name = "test2",   Email = "test2@itu.dk",   Cheeps = new List<Cheep>() };
        context.Authors.AddRange(a1, a2);
        
        context.Cheeps.AddRange(
            new Cheep { Author = a1, Text = "hello", Date = new DateTime(2025, 10, 10),LikedBy = new List<string>() },
            new Cheep { Author = a1, Text = "world", Date = new DateTime(2025, 10, 11),LikedBy = new List<string>() },
            new Cheep { Author = a2, Text = "cheep", Date = new DateTime(2025, 10, 12),LikedBy = new List<string>() }
        );
        context.SaveChanges();
        var repository = new AuthorRepository(context);
        
        //Act
        var dto =  await repository.FindByEmail("test1@itu.dk");
        
        //Assert
        Assert.NotNull(dto);
        Assert.Equal("test1", dto.Name);
        Assert.Equal("test1@itu.dk", dto.Email);
        Assert.Equal(2, dto.Cheeps.Count);

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task FollowUserTest()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var a1 = new Author { Id = "1", Name = "test1", Email = "test1@itu.dk", Cheeps = new List<Cheep>(),Following = new List<string>()};
        var a2 = new Author { Id = "2", Name = "test2",   Email = "test2@itu.dk",   Cheeps = new List<Cheep>(),Following = new List<string>() };
        context.Authors.AddRange(a1, a2);
        context.SaveChanges();
        var repository = new AuthorRepository(context);

        //Act
        await repository.FollowUser(
            new AuthorDTO()
                { Cheeps = new List<CheepDTO>(), Email = a1.Email, Following = a1.Following, Id = a1.Id, Name = a1.Name },
            a2.Name);
        
        //Assert
        Assert.True(a1.Following.Contains(a2.Name));

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task UnFollowUserTest()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var a1 = new Author { Id = "1", Name = "test1", Email = "test1@itu.dk", Cheeps = new List<Cheep>(),Following = new List<string>()};
        var a2 = new Author { Id = "2", Name = "test2",   Email = "test2@itu.dk",   Cheeps = new List<Cheep>(),Following = new List<string>() };
        context.Authors.AddRange(a1, a2);
        context.SaveChanges();
        var repository = new AuthorRepository(context);

        //Act
        await repository.FollowUser(
            new AuthorDTO()
                { Cheeps = new List<CheepDTO>(), Email = a1.Email, Following = a1.Following, Id = a1.Id, Name = a1.Name },
            a2.Name);

        await repository.UnFollowUser(new AuthorDTO()
            {
                Cheeps = new List<CheepDTO>(), Email = a1.Email, Following = a1.Following, Id = a1.Id, Name = a1.Name
            },
            a2.Name);

        //Assert
        Assert.False(a1.Following.Contains(a2.Name));

        //Clean up
        Dispose();
    }

    [Fact]
    public async Task DeleteAuthorTest()
    {
        //Arrange
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var repository = new AuthorRepository(context);
        var a1 = new Author
        {
            Id = "1", Name = "test1", Email = "test1@itu.dk", Cheeps = new List<Cheep>(), Following = new List<string>()
        };
        var a2 = new Author
        {
            Id = "2", Name = "test2", Email = "test2@itu.dk", Cheeps = new List<Cheep>(), Following = new List<string>()
        };
        context.Authors.AddRange(a1, a2);
        context.SaveChanges();

        var authorDTOTest = new AuthorDTO()
        {
            Id = "1",
            Name = "John Doe",
            Email = "test@itu.dk",
            Cheeps = new List<CheepDTO>()
        };
       
        var authorsBefore = await repository.GetAllAuthors();
        var numberOfAuthorsBefore = authorsBefore.Count();
        Assert.Equal(2, numberOfAuthorsBefore);

        //Act
        await repository.DeleteAuthor(authorDTOTest);

        //Assert
        var authorsAfter = await repository.GetAllAuthors();
        var numberOfAuthorsAfter = authorsAfter.Count();
        Assert.Equal(1, numberOfAuthorsAfter);

        //Clean up
        Dispose();

    }
}