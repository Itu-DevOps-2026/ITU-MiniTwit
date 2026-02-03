using System.Data.Common;
using MiniTwit.Infrastructure.Data;
using MiniTwit.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MiniTwit.Infrastructure.Tests.Data;

public class DbInitializerTest : IDisposable
{
    //the set-up is the same as in Chirp/tests/MiniTwit.Infrastructure.Tests/Repositories/AuthorRepositoryTest.cs
    private readonly DbConnection _connection;
    private readonly DbContextOptions<MiniTwitDBContext> _options;

    public DbInitializerTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<MiniTwitDBContext>().UseSqlite(_connection)
            .Options;
    }

    MiniTwitDBContext CreateDbContext() => new MiniTwitDBContext(_options);

    public void Dispose() => _connection.Dispose();
    
    
    [Fact]
    public async Task TestDbInitializerSeedsEmptyDatabaseWithExampleData()
    {
        
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        
        bool isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.True(isDatabaseEmpty);
        
        var userManager = CreateUserManager(context);
        
        var initializer = new DbInitializer(context,userManager);
        
        await initializer.SeedDatabase();
        
        isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.False(isDatabaseEmpty);
        
        HashSet<Author> authors = context.Authors.ToHashSet();
        Assert.Equal(13, authors.Count);
        Assert.Contains(authors, author => author.Name == "Roger Histand");
        Assert.Contains(authors, author => author.Email == "Mellie+Yost@ku.dk");
        Assert.Contains(authors, author => author.Name == "Adrian");
        
        HashSet<Cheep> cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(657, cheeps.Count);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 1);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 453);
        Assert.Contains(cheeps, cheep => cheep.CheepId == 657);
        
        Assert.Contains(cheeps, cheep => cheep.Text == "In the first watch, and every creditor paid in full.");
        Assert.Contains(cheeps, cheep => cheep.Text == "It is he, then?");
    }

    [Fact]
    public async Task TestDbInitializerDoesNotSeedDatabaseIfNotEmpty()
    {
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        var isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.True(isDatabaseEmpty);
        
        var b1 = new Author() { Id = "7341", Name = "Test user", Email = "testuser@itu.dk", Cheeps = new List<Cheep>() };
        var b2 = new Author() { Id = "9999", Name = "Example person", Email = "exampleperson@itu.dk", Cheeps = new List<Cheep>() };
        var authorsList = new List<Author>() { b1, b2 };
        
        var d1 = new Cheep() { CheepId = 7341, Author = b1, Text = "I want to test if the database will not get the seeded data.", Date = DateTime.Parse("2025-08-01 13:13:13"), LikedBy = []};
        var d2 = new Cheep() { CheepId = 9999, Author = b2, Text = "The database contain these cheeps instead.", Date = DateTime.Parse("2025-08-01 14:14:14"),LikedBy = [] };
        
        var cheepsList = new List<Cheep>() { d1, d2 };
        b1.Cheeps = new List<Cheep>() { d1 };
        b2.Cheeps = new List<Cheep>() { d2 };
        
        context.Authors.AddRange(authorsList);
        context.Cheeps.AddRange(cheepsList);
        context.SaveChanges();
        
        isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.False(isDatabaseEmpty);
        HashSet<Author> authors = context.Authors.ToHashSet();
        Assert.Equal(2, authors.Count);
        HashSet<Cheep> cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(2, cheeps.Count);
        
        var userManager = CreateUserManager(context);
        
        var initializer = new DbInitializer(context,userManager);
        
        await initializer.SeedDatabase();
        
        authors = context.Authors.ToHashSet();
        Assert.Equal(2, authors.Count);
        Assert.DoesNotContain(authors, author => author.Name == "Roger Histand");
        Assert.DoesNotContain(authors, author => author.Email == "Mellie+Yost@ku.dk");
        Assert.DoesNotContain(authors, author => author.Name == "Adrian");
        
        cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(2, cheeps.Count);
        Assert.DoesNotContain(cheeps, cheep => cheep.CheepId == 1);
        Assert.DoesNotContain(cheeps, cheep => cheep.CheepId == 453);
        Assert.DoesNotContain(cheeps, cheep => cheep.CheepId == 657);
        
        Assert.DoesNotContain(cheeps, cheep => cheep.Text == "In the first watch, and every creditor paid in full.");
        Assert.DoesNotContain(cheeps, cheep => cheep.Text == "It is he, then?");
        
    }

    [Fact]
    public async Task TestDbInitializerDoesNotDoubleSeededDataIfCalledTwice()
    {
        using var context = CreateDbContext();
        context.Database.EnsureCreated();
        bool isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.True(isDatabaseEmpty);
        
        var userManager = CreateUserManager(context);
        
        var initializer = new DbInitializer(context,userManager);
        
        await initializer.SeedDatabase();
        
        isDatabaseEmpty = !context.Authors.Any() && !context.Cheeps.Any();
        Assert.False(isDatabaseEmpty);
        HashSet<Author> authors = context.Authors.ToHashSet();
        Assert.Equal(13, authors.Count);
        HashSet<Cheep> cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(657, cheeps.Count);
        
        await initializer.SeedDatabase();
        
        authors = context.Authors.ToHashSet();
        Assert.Equal(13, authors.Count);
        cheeps = context.Cheeps.ToHashSet();
        Assert.Equal(657, cheeps.Count);
        
    }
private static UserManager<Author> CreateUserManager(DbContext context)
{
    var store = new UserStore<Author>(context);

    var identityOptions = Options.Create(new IdentityOptions());

    var passwordHasher = new PasswordHasher<Author>();

    var userValidators = new List<IUserValidator<Author>>
    {
        new UserValidator<Author>()
    };

    var passwordValidators = new List<IPasswordValidator<Author>>
    {
        new PasswordValidator<Author>()
    };

    var lookupNormalizer = new UpperInvariantLookupNormalizer();
    var errorDescriber = new IdentityErrorDescriber();

    var services = new ServiceCollection()
        .AddLogging()
        .BuildServiceProvider();

    var logger = services.GetRequiredService<ILogger<UserManager<Author>>>();

    return new UserManager<Author>(
        store,
        identityOptions,
        passwordHasher,
        userValidators,
        passwordValidators,
        lookupNormalizer,
        errorDescriber,
        services,
        logger
    );
}
}