using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieSite.Database;
using MovieSite.Database.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSite.ApiService.Controllers;

[Route("[controller]")]
[ApiController]
public class ScreenController(MovieDbContext db, ILogger<ScreenController> logger) : ControllerBase
{
    private readonly MovieDbContext _db = db;
    private readonly ILogger<ScreenController> _logger = logger;

    // GET: api/<MoviesController>
    [HttpGet]
    public async Task<List<Screen>> Get()
    {
        return await _db.Screens.ToListAsync();
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
