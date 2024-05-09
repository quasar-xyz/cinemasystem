using System.Diagnostics;
using System.Security.Claims;
using CinemaSystem.Data;
using Microsoft.AspNetCore.Mvc;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CinemaSystem.Models;

namespace CinemaSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Authorize]
    public async Task<IActionResult> BookMovie(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var booking = new Booking
        {
            UserId = userId,
            MovieId = movie.Id,
            BookingDate = DateTime.Now
        };

        _context.Add(booking);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(MyBookings));
    }


    [Authorize]

public async Task<IActionResult> MyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        var bookings = await _context.Bookings
            .Include(b => b.Movie)
            .Where(b => b.UserId == userId)
            .ToListAsync();

        return View(bookings);
    }
    
    public async Task<IActionResult> DetailsMovie(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.ToListAsync();
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}