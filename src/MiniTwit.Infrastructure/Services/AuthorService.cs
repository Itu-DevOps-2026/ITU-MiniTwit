using MiniTwit.Core.DTO;
using MiniTwit.Core.Interfaces;

namespace MiniTwit.Infrastructure.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
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