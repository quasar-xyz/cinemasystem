namespace CinemaSystem.Models;

public class Seat
{
    
    public int SeatId { get; set; }
    public int Number { get; set; }
    public string Row { get; set; }
    public Reservation Reservation { get; set; }
}