using Microsoft.AspNetCore.Identity;

namespace CinemaSystem.Data.Identity;

public class CustomRole : IdentityRole
{
    public string Color { get; set; }
}