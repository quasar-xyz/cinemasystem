using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    
    public string ImagePath { get; set; } = string.Empty;
    
 
}