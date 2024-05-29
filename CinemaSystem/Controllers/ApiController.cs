using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem.Controllers;

[ApiController]
[Route("api/movies")]
public class ApiController : Controller
{
    private readonly ApplicationDbContext _context;

    public ApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("allmovies")]
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return Json(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }
    
  
}
