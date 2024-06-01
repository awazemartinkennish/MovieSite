using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MovieSite.Database;
using MovieSite.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSite.ApiService.MovieScreenings;

[Route("[controller]")]
[ApiController]
public class MovieScreeningController(MovieDbContext db, ILogger<MovieScreeningController> logger) : ControllerBase
{
    private readonly MovieDbContext _db = db;
    private readonly ILogger<MovieScreeningController> _logger = logger;

    // GET: api/<MoviesController>
    [HttpGet]
    public async Task<List<GetMovieScreeningView>> Get(DateOnly? showingDate)
    {
        _logger.LogDebug("Fetching screenings from database");
        IQueryable<MovieScreening> interim = _db.MovieScreenings
            .Include(ms => ms.Movie!)
            .Include(ms => ms.Screen!)
            .Include(ms => ms.Tickets);

        if (showingDate.HasValue)
        {
            _logger.LogDebug("Filtering for the date {Date}", showingDate);
            interim = interim.Where(ms => ms.ScreeningTime.Date == showingDate.Value.ToDateTime(TimeOnly.MinValue));
        }

        var results = await interim
            .Select(ms => new GetMovieScreeningView() { 
                Id = ms.Id,
                MovieTitle = ms.Movie!.Title,
                Location = ms.Screen!.Name,
                StartTime = ms.ScreeningTime,
                SeatsSold = ms.Tickets!.Count(),
                Capacity = ms.Screen!.Capacity,
                TicketPrice = ms.TicketPrice,
                ReviewScore = ms.Movie!.ReviewScore,
                BoardRating = ms.Movie!.BoardRating
            })
            .OrderBy(ms => ms.StartTime)
            .ToListAsync();
        _logger.LogDebug("Fetched {Count} movies", results.Count);

        return results;
    }

    //// GET api/<MoviesController>/5
    //[HttpGet("{id}")]
    //public string Get(int id)
    //{
    //    return "value";
    //}

    //// POST api/<MoviesController>
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT api/<MoviesController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE api/<MoviesController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
