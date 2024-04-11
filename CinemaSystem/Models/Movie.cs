using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models;

public class Movie
{
    public int MovieId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public int DurationMinutes { get; set; }
    
    public ICollection<Screening> Screenings { get; set; }
}