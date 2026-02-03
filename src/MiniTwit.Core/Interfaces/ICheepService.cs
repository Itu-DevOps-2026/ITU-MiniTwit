using MiniTwit.Core.DTO;

namespace MiniTwit.Core.Interfaces;

public interface ICheepService
{
    public List<CheepDTO> GetCheeps(out bool hasNext, int? page);
    public List<CheepDTO> GetCheepsFromAuthor(string author, out bool hasNext, int? page);
    public List<CheepDTO> GetCheepsFromAuthorOnOnePage(string author);
    public Task CreateCheepFromDTO(CheepDTO cheep);
    public List<CheepDTO> GetCheepsFromAuthors(IList<string> authors, out bool hasNext, int? page = null);
    public Task LikeCheep(int cheep,string likedBy);
    public Task UnLikeCheep(int cheep, string likedBy);

}