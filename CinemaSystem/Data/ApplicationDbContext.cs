using CinemaSystem.Data.Identity;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Data;

public class ApplicationDbContext : IdentityDbContext<CustomUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    
    public DbSet<Testmodel> testmodels { get; set; }
    
    public DbSet<Seat> Seats { get; set; }
    
    public DbSet<ShowTime> ShowTimes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Movie>().ToTable("Movies");
        modelBuilder.Entity<Booking>().ToTable("Bookings");
    }
}