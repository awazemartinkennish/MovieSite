using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieSite.Database;
using MovieSite.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSite.ApiService.Screens;

[Route("[controller]")]
[ApiController]
public class ScreenController(MovieDbContext db, ILogger<ScreenController> logger) : ControllerBase
{
    private readonly MovieDbContext _db = db;
    private readonly ILogger<ScreenController> _logger = logger;

    // GET: api/<MoviesController>
    [HttpGet]
    public async Task<IEnumerable<GetScreenView>> Get()
    {
        _logger.LogDebug("Fetching screens from database");
        List<Screen> dbModels = await _db.Screens
            .Include(s => s.Screenings!)
            .ThenInclude(ms => ms.Movie)
            .ToListAsync();
        _logger.LogDebug("Fetched {Count} screens", dbModels.Count);

        return dbModels.Select(s => new GetScreenView()
        {
            Id = s.Id,
            Name = s.Name,
            Capacity = s.Capacity,
            Screenings = s.Screenings?.Select(ms => new GetScreenView.ScreeningView()
            {
                ScreeningId = ms.Id,
                MovieTitle = ms.Movie!.Title,
                ScreeningTime = ms.ScreeningTime
            }).ToList() ?? []
        });
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
