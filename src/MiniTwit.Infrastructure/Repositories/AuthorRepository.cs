using MiniTwit.Core.DTO;
using MiniTwit.Core.Interfaces;
using MiniTwit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MiniTwit.Infrastructure.Entities;

namespace MiniTwit.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly MiniTwitDBContext _context;
    
    public AuthorRepository(MiniTwitDBContext context)
    {
        _context = context;
    }

    // Create a new author
    public async Task CreateAuthor(AuthorDTO newUser)
    {
        // Creates a new author
        Author author = new()
        {
            Name = newUser.Name,
            Email = newUser.Email,
            Cheeps = new List<Cheep>()
        };

        // Adds and saves the cheep in the database
        var queryResult = await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    // Get all authors
    public async Task<List<AuthorDTO>> GetAllAuthors()
    {
        // AI helped with syntax problems in this method
        // Construction of query gets all authors
        var authorsQuery = (from author in _context.Authors select author)
            .Include(a => a.Cheeps);
            
        var authors = await authorsQuery.ToListAsync();
        
        var query = from author in authors
            select new AuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email!,
                Cheeps = author.Cheeps
                    .Select(c => new CheepDTO
                    {
                        Id = c.CheepId,
                        AuthorId = author.Id,
                        Text = c.Text,
                        CreatedAt = c.Date
                    })
                    .ToList()
            };

        var result =  query.ToList();
        
        return result;
    }

    // Update author
    public async Task UpdateAuthor(AuthorDTO updatedAuthor)
    {
        // Construction of the query that selects cheeps written by the same AuthorID
        var query = from author in _context.Authors
            where author.Id == updatedAuthor.Id 
            select author;
        
        var originalAuthor = await query.FirstOrDefaultAsync();

        // Error handling
        if (originalAuthor == null)
        {
            throw new Exception("Unable to find the cheep");
        }

        // Call to utility method that updates the properties of the original author
        UpdateAuthor(originalAuthor, updatedAuthor);

        // Saves changes
        await _context.SaveChangesAsync();
    }

    // Utility method: set the new properties of the Author
    private void UpdateAuthor(Author originalAuthor, AuthorDTO updatedAuthor)
    {
        // Sets the new properties
        originalAuthor.Id = updatedAuthor.Id;
        originalAuthor.Name = updatedAuthor.Name;
        originalAuthor.Email = updatedAuthor.Email;

        ICollection<Cheep> cheeps = new List<Cheep>();

        foreach (var cheep in updatedAuthor.Cheeps)
        {
            Cheep newCheep = FromCheepDtoToCheep(cheep).Result;
            cheeps.Add(newCheep);
        }
        
        originalAuthor.Cheeps = cheeps;
    }
    
    // Translate from CheepDTO to Cheeps by querying DB
    private async Task<Cheep> FromCheepDtoToCheep(CheepDTO oldCheep)
    {
        var query = from cheep in _context.Cheeps
            where cheep.CheepId == oldCheep.Id
            select cheep;
        var originalCheep = await query.FirstOrDefaultAsync();
        if (originalCheep == null)
        {
            throw new Exception("Unable to find the original cheep");
        }
        return originalCheep;
    } 
    
    //Query selecting author whose name matches the provided
    public async Task<AuthorDTO?> FindByName(string name)
    {
        // Construction of query gets the matching author incl. cheeps
        var authorsQuery = (from author in _context.Authors
                where author.Name == name
                select author)
            .Include(a => a.Cheeps);

        var authors = await authorsQuery.ToListAsync();
        var query = from author in authors
            select new AuthorDTO()
            {
                Id = author.Id, 
                Name = author.Name,
                Email = author.Email,
                Cheeps = author.Cheeps 
                    .Select(c => new CheepDTO 
                    {
                        Id = c.CheepId,
                        AuthorId = author.Id,
                        Text = c.Text,
                        CreatedAt = c.Date
                    })
                    .ToList()
            };
        var result = query.FirstOrDefault();
        return result;
    }
    
    //Query that selects the author whose email matches the provided
    public async Task<AuthorDTO?> FindByEmail(string email){
        // Construction of query gets the matching author incl. cheeps
        var authorsQuery = (from author in _context.Authors
                where author.Email == email
                select author)
            .Include(a => a.Cheeps);
        
        var authors = await authorsQuery.ToListAsync();

        var query = from author in authors
            select new AuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                //for each cheep, create new CheepDTO object
                Cheeps = author.Cheeps
                    //projects the Author entity into an AuthorDTO including the cheeps
                    .Select(c => new CheepDTO
                    {
                        Id = c.CheepId,
                        AuthorId = author.Id,
                        Text = c.Text,
                        CreatedAt = c.Date
                    })
                    .ToList()
            };
        var result = query.FirstOrDefault();
        return result;
    }
    
    // Insert new user to follow
    public async Task FollowUser(AuthorDTO self, string followAuthorUsername)
    {
        var query = from author in _context.Authors
            where author.Id == self.Id 
            select author;
        
        var originalAuthor = await query.FirstOrDefaultAsync();

        if (originalAuthor!.Following.Contains(followAuthorUsername))
        {
            return;
        }
        
        originalAuthor!.Following.Add(followAuthorUsername);
        await _context.SaveChangesAsync();
    }
    
    // Remove user from followed users
    public async Task UnFollowUser(AuthorDTO self, string followAuthorUsername)
    {
        var query = from author in _context.Authors
            where author.Id == self.Id 
            select author;
        
        var originalAuthor = await query.FirstOrDefaultAsync();
        
        originalAuthor!.Following.Remove(followAuthorUsername);
        await _context.SaveChangesAsync();
    }
    
    // Query to delete author
    public async Task DeleteAuthor(AuthorDTO self)
    {
        
        var query = from author in _context.Authors
            where author.Id == self.Id
            select author;
        var originalAuthor = await query.FirstOrDefaultAsync();
        
        if (originalAuthor == null)
        {
            throw new Exception("Unable to find the original author");
        }
        
        _context.Remove(originalAuthor);
        
        await _context.SaveChangesAsync();
    }
}