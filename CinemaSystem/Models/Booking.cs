using System.ComponentModel.DataAnnotations;
using CinemaSystem.Data.Identity;

namespace CinemaSystem.Models;

public class Booking
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int SeatId { get; set; } 
    public Seat Seat { get; set; } 
    public int ShowTimeId { get; set; } 
    public ShowTime ShowTime { get; set; } 
    
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    
    public CustomUser User { get; set; }
    
    [Display(Name = "Booking Date")]
    [DataType(DataType.DateTime)]
    public DateTime BookingDate { get; set; }
    
    
}