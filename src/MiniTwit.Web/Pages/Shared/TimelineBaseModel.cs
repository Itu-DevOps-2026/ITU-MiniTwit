using MiniTwit.Core.DTO;
using MiniTwit.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using MiniTwit.Core.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace MiniTwit.Web.Pages.Shared;

public class TimelineBaseModel : PageModel
{
    protected readonly ICheepService _cheepService;
    protected readonly IAuthorService _authorService;
    public List<CheepDTO>? Cheeps { get; set; }
    [BindProperty]
    [Required(ErrorMessage ="Your cheep can't be empty.")]
    [StringLength(160, ErrorMessage = "Your cheep is too long. Maximum length is 160 characters.")]
    public required string CheepText { get; set; }
    [BindProperty]
    public string? UserId { get; set; }
    public string? DisplayName { get; set; }
    [TempData]
    public string? StatusMessage { get; set; }
    
    protected readonly UserManager<Author> UserManager;
    public string? ErrorMessage { get; set; }
    
    //Inject the cheep service, sets a specific "model"
    public TimelineBaseModel(ICheepService cheepService, IAuthorService authorService, UserManager<Author> userManager)
    {
        _cheepService = cheepService;
        _authorService = authorService;
        UserManager = userManager;
        //Changed form CheepViewModel to CheepDTO to better support like functionality 
        Cheeps = new List<CheepDTO>();
    }

    //Get current user information
    //Obs: Used ChatGPT to help figure out how to get userManager info from public.cshtml (html) to this class (c#)
    public async Task GetUserInformation()
    {
        if (User.Identity!.IsAuthenticated)
        {
            var currentUser = await UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                // The user is authenticated but not found in the database, sign them out
                await HttpContext.SignOutAsync();
                Response.Redirect("/Account/Login");
                return;
            }

            DisplayName = currentUser.Name;
            UserId = currentUser.Id;
        }
    }

    //Handle error messages
    public void HandleError(string? error)
    {
        if (error == null)
        {
            return;
        }
        
        ErrorMessage = error;
        if (ErrorMessage == "empty_cheep")
        {
            ErrorMessage = "Your cheep can't be empty.";
        }
    }
    
    //Post method for creating a cheep (unless the ModelState is invalid, then it will show an error message)
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Redirect(Request.Path + "?error=empty_cheep"); 
        }
        //Create CheepDTO
        var cheepDTO = new CheepDTO()
        {
            CreatedAt = DateTime.Now,
            Text = CheepText,
            AuthorId = UserId!
        };
       
        //Call the repository method for creating a cheep
        await _cheepService.CreateCheepFromDTO(cheepDTO);
        
        StatusMessage = "MiniTwit was successfully created!";
        
        return RedirectToPage();
    }
}