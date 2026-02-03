using MiniTwit.Core.DTO;
using MiniTwit.Core.Interfaces;

namespace MiniTwit.Infrastructure.Services;

public record CheepViewModel(string Author, string Message, string Timestamp,List<string> LikedBy);

public class CheepService : ICheepService
{
    private readonly ICheepRepository _cheepRepository;

    public CheepService(ICheepRepository cheepRepository)
    {
        _cheepRepository = cheepRepository;       
    }
    
    // Fetches all cheeps from repository
    public List<CheepDTO> GetCheeps(out bool hasNext, int? page = null)
    {
        var cheeps = _cheepRepository.GetAllCheeps(page).Result;

        hasNext = cheeps.Count() == 32;
        
        return cheeps; 
    }

    // Fetches cheeps by specified author form repository
    public List<CheepDTO> GetCheepsFromAuthor(string author, out bool hasNext, int? page = null)
    {
        var cheeps = _cheepRepository.ReadCheepsBy(author,page).Result;

        hasNext = cheeps.Count() == 32;
        return cheeps;
    }
    
    // Fetches all cheeps by specified author on one page from repository 
    public List<CheepDTO> GetCheepsFromAuthorOnOnePage(string author)
    {
        var cheeps = _cheepRepository.ReadCheepsByOnOnePage(author).Result;

        return cheeps;
    }
    
    // Fetches cheeps by specified authors form repository
    public List<CheepDTO> GetCheepsFromAuthors(IList<string> authors, out bool hasNext, int? page = null)
    {
        var cheeps = _cheepRepository.ReadCheepsBySelfAndOthers(authors,page).Result;
        
        hasNext = cheeps.Count() == 32;
        return cheeps;
    }
    
    // Creates a new cheep
    public async Task CreateCheepFromDTO(CheepDTO cheep)
    {
        await _cheepRepository.CreateCheep(cheep);
    }

    // Likes a specific Cheep by its id
    public async Task LikeCheep(int cheepId,string likedBy)
    {
        var cheep = await _cheepRepository.GetCheep(cheepId);
        cheep.LikedBy.Add(likedBy);
        await _cheepRepository.UpdateCheep(cheep);
    }

    // Unlikes a specific cheep by its id
    public async Task UnLikeCheep(int cheepId, string likedBy)
    {
        var cheep = await _cheepRepository.GetCheep(cheepId);
        cheep.LikedBy.Remove(likedBy);
        await _cheepRepository.UpdateCheep(cheep);
    }

}
