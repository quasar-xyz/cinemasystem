namespace CinemaSystem.Models;

public class Seat
{
    public int Id { get; set; }
    public bool IsBooked { get; set; }
    public int ShowTimeId { get; set; }
    public ShowTime ShowTime { get; set; }
}