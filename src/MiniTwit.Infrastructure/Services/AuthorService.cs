using MiniTwit.Core.DTO;
using MiniTwit.Core.Interfaces;
using MiniTwit.Infrastructure.Entities;

namespace MiniTwit.Infrastructure.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    
    // Create a new author
    public async Task CreateAuthor(string username, string email, string password)
    {
        AuthorDTO newAuthor = new AuthorDTO()
        {
            Id = "1", // 1 because id is thrown out and created properly by Identity
            Name = username,
            Email = email,
            Cheeps = new List<CheepDTO>()
        };
        
        await _authorRepository.CreateAuthor(newAuthor);
    }
    
    //Retrieves an author based on a username
    public async Task<AuthorDTO?> GetAuthorByName(string authorUsername)
    {
        return await _authorRepository.FindByName(authorUsername);
    }
    
    //Follows an author 
    public async Task Follow(string thisUsername, string otherUsername)
    {
        AuthorDTO? self = await GetAuthorByName(thisUsername);
        await _authorRepository.FollowUser(self!, otherUsername);
    }

    //Unfollow an author
    public async Task Unfollow(string thisUsername, string otherUsername)
    {
        AuthorDTO? self = await GetAuthorByName(thisUsername);
        await _authorRepository.UnFollowUser(self!, otherUsername);
    }

    //Delete an author
    public async Task DeleteAuthor(string thisUsername)
    {
        AuthorDTO? self = await GetAuthorByName(thisUsername);
        await _authorRepository.DeleteAuthor(self!);
    }
}