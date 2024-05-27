using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieSite.Database;
using MovieSite.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSite.ApiService.Movie;

[Route("[controller]")]
[ApiController]
public class MovieController(MovieDbContext db, ILogger<MovieController> logger) : ControllerBase
{
    private readonly MovieDbContext _db = db;
    private readonly ILogger<MovieController> _logger = logger;

    // GET: api/<MoviesController>
    [HttpGet]
    public async Task<List<GetMovieView>> Get()
    {
        var dbModels = await _db.Movies.Include(m => m.Screenings).ThenInclude(s=> s.Screen).ToListAsync();

        return dbModels.Select(m => new GetMovieView()
        {
            Id = m.Id,
            Title = m.Title,
            Genre = m.Genre,
            ReleaseDate = m.ReleaseDate,
            ReviewScore = m.ReviewScore,
            BoardRating = m.BoardRating,
            Screenings = m.Screenings.Select(s => new GetMovieView.ScreeningView()
            {
                ScreeningId = s.Id,
                Location = s.Screen!.Name,
                ScreeningTime = s.ScreeningTime
            }).ToList()
        }).ToList();
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
