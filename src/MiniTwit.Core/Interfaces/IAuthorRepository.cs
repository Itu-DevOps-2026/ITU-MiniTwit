using MiniTwit.Core.DTO;

namespace MiniTwit.Core.Interfaces;

public interface IAuthorRepository
{
    Task CreateAuthor(AuthorDTO newUser);
    Task<List<AuthorDTO>> GetAllAuthors();
    Task UpdateAuthor(AuthorDTO updatedAuthor);
    Task<AuthorDTO?> FindByName(string name);
    Task<AuthorDTO?> FindByEmail(string email);
    Task FollowUser(AuthorDTO self,string followAuthorUsername);
    Task UnFollowUser(AuthorDTO self,string followAuthorUsername);
    Task DeleteAuthor(AuthorDTO author);
}