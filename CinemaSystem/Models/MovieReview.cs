namespace CinemaSystem.Models;

public class MovieReview
{
    public Movie Movie { get; set; }
    public List<Review> Reviews { get; set; }
}