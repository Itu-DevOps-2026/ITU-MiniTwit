using MiniTwit.Core.Interfaces;
using MiniTwit.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MiniTwit.Web.Pages.Shared;

namespace MiniTwit.Web.Pages;

// Page model for 'Public timeline' containing all cheeps from all authors
public class PublicModel : TimelineBaseModel
{
    
    // Inherits from parent class TimelineBaseModel, which injects the Services and sets the model
    public PublicModel(ICheepService cheepService, IAuthorService authorService, UserManager<Author> userManager) : base(cheepService, authorService, userManager)
    {
    }
    
    // Used for page links
    public int PageNumber { get; set; }
    public bool HasMorePages { get; set; }

    // Get all cheeps by all authors
    public async Task<ActionResult> OnGetAsync([FromQuery] int page = 1, [FromQuery] string? error = null)
    {
        HandleError(error);
        
        // Call base method to get user info
        await GetUserInformation();
        
        Cheeps = _cheepService.GetCheeps(out bool hasNext, page);
        
        // Used to show/hide next-page button
        HasMorePages = hasNext;

        // Used for page links
        PageNumber = page;
        
        return Page();
    }

    // OnPost-method for when a user likes a cheep
    public async Task<ActionResult> OnPostLikeAsync(int cheep, string returnUrl)
    {
        var currentUser = await UserManager.GetUserAsync(User);
        await _cheepService.LikeCheep(cheep, currentUser!.Name);
        return LocalRedirect(returnUrl);
    }

    // OnPost-method for when a user unlikes a cheep
    public async Task<ActionResult> OnPostUnLikeAsync(int cheep,string returnUrl)
    {
        var currentUser = await UserManager.GetUserAsync(User);
        await _cheepService.UnLikeCheep(cheep, currentUser!.Name);
        return LocalRedirect(returnUrl);
    }
    
}