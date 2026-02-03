using MiniTwit.Core.DTO;

namespace MiniTwit.Core.Interfaces;

public interface IAuthorService
{
    public Task<AuthorDTO?> GetAuthorByName(string authorUsername);
    public Task Follow(string thisUsername, string otherUsername);
    public Task Unfollow(string thisUsername, string otherUsername);
    public Task DeleteAuthor(string thisUsername);
}