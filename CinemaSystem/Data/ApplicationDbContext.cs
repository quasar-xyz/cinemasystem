using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Showtime> Showtimes { get; set; } 
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat> Seats { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Showtime)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.ShowtimeId); 

            modelBuilder.Entity<Showtime>() 
                .HasOne(s => s.Movie)
                .WithMany(m => m.Showtimes) 
                .HasForeignKey(s => s.MovieId); 
        }
    }
}