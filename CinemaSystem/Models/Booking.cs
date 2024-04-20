using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models;

public class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    [Required]
    [Display(Name = "Seat Number")]
    public string SeatNumber { get; set; }

    [Display(Name = "Booking Date")]
    [DataType(DataType.DateTime)]
    public DateTime BookingDate { get; set; }
}