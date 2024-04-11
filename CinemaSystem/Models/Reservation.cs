using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models;

public class Reservation
{
    public int ReservationId { get; set; }

    public int UserId { get; set; }

    
    public User User { get; set; }

    public int ScreeningId { get; set; }

    
    public Screening Screening { get; set; }

    [Required]
    public string SeatNumber { get; set; }

    [Required]
    public DateTime ReservationTime { get; set; }
}