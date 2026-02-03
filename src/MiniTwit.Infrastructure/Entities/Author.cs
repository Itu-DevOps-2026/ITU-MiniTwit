using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MiniTwit.Infrastructure.Entities;

// Inherit from IdentityUser to enable EF Core Identity (Login functionality)
[Index(nameof(Name), IsUnique = true)]
public class Author : IdentityUser 
{
    // IdentityUser already have an ID and email fields
    
    public string Name { get; set; } = string.Empty;
    public ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();
    public IList<string> Following { get; set; } = new List<string>();
}