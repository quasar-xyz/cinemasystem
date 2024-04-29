using Microsoft.AspNetCore.Identity;

namespace CinemaSystem.Data.Identity;

public class CustomUser : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
}