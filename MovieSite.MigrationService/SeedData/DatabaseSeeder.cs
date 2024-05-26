using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using MovieSite.Database.Models;

namespace MovieSite.MigrationService.SeedData
{
    public class DatabaseSeeder
    {
        public DatabaseSeeder()
        {
            Randomizer.Seed = new Random(20240526);
        }

        public (List<Movie> movies, List<Screen> screens, List<MovieScreening> screenings, List<Ticket> tickets) GenerateData()
        {
            int movieIds = 0;

            var movies = new Faker<Movie>()
                .RuleFor(m => m.Id, f => movieIds++)
                .RuleFor(m => m.Title, f => f.Random.Words(5))
                .RuleFor(m => m.Genre, f => f.PickRandom("Action", "Comedy", "Drama", "Horror", "Sci-Fi"))
                .RuleFor(m => m.ReleaseDate, f => f.Date.BetweenDateOnly(
                    DateOnly.FromDateTime(DateTime.Now.AddMonths(-3)),
                    DateOnly.FromDateTime(DateTime.Now)))
                .RuleFor(m => m.ReviewScore, f => f.Random.Double(1, 5))
                .RuleFor(m => m.BoardRating, f => f.PickRandom<Movie.Rating>())
                .Generate(10);

            int screenIds = 0;
            var screens = new Faker<Screen>()
                .RuleFor(s => s.Id, f => screenIds++)
                .RuleFor(s => s.Name, f => $"Screen {screenIds}")
                .RuleFor(s => s.Capacity, f => f.Random.Number(50, 500))
                .Generate(10);

            int screeningIds = 0;
            DateTimeOffset screeningTime = DateTimeOffset.UtcNow;
            int screeningScreenId = 1;
            var screenings = new Faker<MovieScreening>()
                .RuleFor(s => s.Id, f => screeningIds++)
                .RuleFor(s => s.MovieId, f => f.Random.Number(0, movies.Count - 1))
                .RuleFor(s => s.ScreenId, f =>
                {
                    if (screeningScreenId == screens.Count)
                    {
                        screeningScreenId = 1;
                    }
                    else
                    {
                        screeningScreenId++;
                    }
                    return screeningScreenId;
                })
                .RuleFor(s => s.ScreeningTime, f =>
                {
                    if (screeningTime.Hour == 23)
                    {
                        screeningTime = screeningTime.AddDays(1);
                        screeningTime = screeningTime.AddHours(-23 + 8);
                    }
                    else
                    {
                        screeningTime = screeningTime.AddHours(1);
                    }

                    return screeningTime;
                })
                .RuleFor(s => s.TicketPrice, f => f.Random.Decimal(5, 20))
                .Generate(50);

            int ticketIds = 0;
            var tickets = new Faker<Ticket>()
                .RuleFor(t => t.Id, f => ticketIds++)
                .RuleFor(t => t.MovieScreeningId, f => f.Random.Number(0, screenings.Count - 1))
                .RuleFor(t => t.SeatNumber, f => f.Random.Number(1, 50))
                .Generate(100);

            return (movies, screens, screenings, tickets);
        }
    }
}
