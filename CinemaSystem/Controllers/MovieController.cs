using System.Security.Claims;
using CinemaSystem.Data;
using CinemaSystem.Data.Identity;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Controllers;

[Authorize(Roles = "Admin")]
public class MovieController : Controller
{
    private readonly UserManager<CustomUser> _userManager;
    private readonly IWebHostEnvironment _env;
    private readonly ApplicationDbContext _context;

    public MovieController(ApplicationDbContext context, IWebHostEnvironment env, UserManager<CustomUser> userManager)
    {
        _context = context;
        _env = env;
    }


    public async Task<IActionResult> Index()
    {
        var movies = await _context.Movies.ToListAsync();
        return View(movies);
    }

    public async Task<IActionResult> Details(int? id)
    {
        
        var movie = await _context.Movies
            .Include(m => m.Reviews) 
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    
    

    public IActionResult Create()
    {
        var images = Directory.EnumerateFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images"))
            .Select(Path.GetFileName);
        ViewBag.ImageFiles = new SelectList(images);
        return View();
        
    }

    
    [HttpPost]
    public async Task<IActionResult> PhotoUpload(IFormFile file)
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
    
    
    public async Task<IActionResult> PhotoUpload()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,ReleaseDate,ImagePath,ShowTime,Review")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            
            _context.Add(movie);
            await _context.SaveChangesAsync();
            movie.ImagePath = "Images/" + movie.ImagePath;
            
            var showTime = new ShowTime
            {
                MovieId = movie.Id,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(3),
            };

            _context.Add(showTime);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }
    
    public async Task<IActionResult> Edit(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);
        return View(movie);
    }

    
    
    [HttpPost]
    public IActionResult AddReview(CinemaSystem.Models.Review review)
    {
        
        _context.Reviews.Add(review);
        _context.SaveChanges();
        
        var movie = _context.Movies.Include(m => m.Reviews).Single(m => m.Id == review.MovieId);
        _context.SaveChanges();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            var movieToUpdate = await _context.Movies.FindAsync(id);

            if (movieToUpdate != null)
            {
                movieToUpdate.Name = movie.Name;
                movieToUpdate.ReleaseDate = movie.ReleaseDate;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }


    
    public async Task<IActionResult> MyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var bookings = await _context.Bookings
            .Include(b => b.Movie)
            .Where(b => b.UserId == userId)
            .ToListAsync();

        return View(bookings);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(MyBookings));
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        var movie = await _context.Movies
            .FirstOrDefaultAsync(m => m.Id == id);
        return View(movie);
    }


    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}