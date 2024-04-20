using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaSystem.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Showtime")]
        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}