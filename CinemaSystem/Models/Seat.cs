namespace CinemaSystem.Models;

public class Seat
{
    public int SeatId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public bool IsBooked { get; set; }
}