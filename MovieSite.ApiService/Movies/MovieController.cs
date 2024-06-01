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
        _logger.LogDebug("Fetching movies from database");
        List<GetMovieView> results = await _db.Movies.Include(m => m.Screenings!)
                                               .ThenInclude(s => s.Screen)
                                              .Select(m => new GetMovieView()
                                              {
                                                  Id = m.Id,
                                                  Title = m.Title,
                                                  Genre = m.Genre,
                                                  ReleaseDate = m.ReleaseDate,
                                                  ReviewScore = m.ReviewScore,
                                                  BoardRating = m.BoardRating,
                                                  Screenings = m.Screenings!.Select(s => new GetMovieView.ScreeningView()
                                                  {
                                                      ScreeningId = s.Id,
                                                      Location = s.Screen!.Name,
                                                      ScreeningTime = s.ScreeningTime
                                                  }).ToList()
                                              }).ToListAsync();
        _logger.LogDebug("Fetched {Count} movies", results.Count);

        return results;
    }

    // GET api/<MoviesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetMovieView>> Get(int id)
    {
        _logger.LogDebug("Fetching movie with id {Id} from database", id);
        GetMovieView? result = await _db.Movies!.Where(m => m.Id == id)
                                         .Include(m => m.Screenings!)
                                         .ThenInclude(s => s.Screen!).Select(m => new GetMovieView()
                                         {
                                             Id = m.Id,
                                             Title = m.Title,
                                             Genre = m.Genre,
                                             ReleaseDate = m.ReleaseDate,
                                             ReviewScore = m.ReviewScore,
                                             BoardRating = m.BoardRating,
                                             Screenings = m.Screenings!.Select(s => new GetMovieView.ScreeningView()
                                             {
                                                 ScreeningId = s.Id,
                                                 Location = s.Screen!.Name,
                                                 ScreeningTime = s.ScreeningTime
                                             }).ToList()
                                         }).FirstOrDefaultAsync();
        _logger.LogDebug("Fetched {count} movies", result == null ? 0 : 0);

        return result switch
        {
            null => NotFound(),
            _ => result
        };
    }

    // POST api/<MoviesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateMovieInput input)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Invalid input model {@ValidationState}", ModelState.ValidationState);
            return BadRequest(ModelState);
        }

        var movie = new Movie()
        {
            Id = 0,
            Title = input.Title,
            Genre = input.Genre,
            ReleaseDate = input.ReleaseDate,
            ReviewScore = input.ReviewScore,
            BoardRating = input.BoardRating.ToRating()
        };

        _logger.LogDebug("Adding movie {@Movie}", movie);
        await _db.Movies.AddAsync(movie);
        await _db.SaveChangesAsync();
        _logger.LogDebug("Added movie with id {Id}", movie.Id);

        return Created(new Uri($"/Movies/{movie.Id}", UriKind.Relative), movie.Id);
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
