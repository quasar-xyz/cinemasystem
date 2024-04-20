using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaSystem.Models
{
    public class Showtime
    {
        public int ShowtimeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}