using Microsoft.AspNetCore.Identity;

namespace Chirp.Infrastructure.Models;

public class Author : IdentityUser {
    
    public ICollection<Cheep> Cheeps { get; set; }
    public ICollection<Author> Followers { get; set; }
    public ICollection<Author> Following { get; set; }
}