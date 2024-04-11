using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models;

public class Screening
{
    public int ScreeningId { get; set; }
    
    public int MovieId { get; set; }
    
    public Movie Movie { get; set; }

    [Required]
    public int ScreenNumber { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    
    public ICollection<Reservation> Reservations { get; set; }
}