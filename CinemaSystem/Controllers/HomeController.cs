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
    private readonly IWebHostEnvironment _env;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment env)
    {
        _logger = logger;
        _context = context;
        _env = env;
    }


    [Authorize]
    public async Task<IActionResult> ShowTimes(int? id)
    {
        var showTimes = await _context.ShowTimes
            .Where(s => s.MovieId == id)
            .ToListAsync();


        return View(showTimes);
    }


    [Authorize]
    public async Task<IActionResult> BookMovie(int? id)
    {
        var showTime = await _context.ShowTimes.FindAsync(id);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var seat = new Seat
        {
            IsBooked = true,
            ShowTimeId = showTime.Id
        };

        _context.Add(seat);
        await _context.SaveChangesAsync();

        var booking = new Booking
        {
            UserId = userId,
            SeatId = seat.Id,
            ShowTimeId = showTime.Id,
            MovieId = showTime.MovieId
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
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);

        return View(movie);
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile file)
    {
        
        string uploadfolder = Path.Combine(_env.WebRootPath, "Images");
        if (!Directory.Exists(uploadfolder))
        {
            Directory.CreateDirectory(uploadfolder);
        }
        string fileName = Path.GetFileName(file.FileName);
        string filePath = Path.Combine(uploadfolder, fileName);
      
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return View();
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