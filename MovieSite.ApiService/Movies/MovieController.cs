using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieSite.Database;
using MovieSite.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSite.ApiService.Movies;

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

    // POST api/<MoviesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateMovieInput input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var movie = new Movie()
        {
            Id = 0,
            Title = input.Title,
            Genre = input.Genre,
            ReleaseDate = input.ReleaseDate,
            ReviewScore = input.ReviewScore,
            BoardRating = input.BoardRating switch
            {
                "UA" => Movie.Rating.UA,
                "U" => Movie.Rating.U,
                "PG" => Movie.Rating.PG,
                "12A" => Movie.Rating.TwelveA,
                "15" => Movie.Rating.Fifteen,
                "18" => Movie.Rating.Eighteen,
                _ => throw new ArgumentException("Invalid rating")
            }
        };

        await _db.Movies.AddAsync(movie);
        _db.SaveChanges();

        return Created();
    }

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
