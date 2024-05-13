namespace CinemaSystem.Models;

public class ShowTime
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ICollection<Seat> Seats { get; set; }
}