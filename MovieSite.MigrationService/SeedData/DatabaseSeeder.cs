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

        private readonly List<string> movieTitles = new()
        {
            "The Godfather",
            "The Shawshank Redemption",
            "Schindler's List",
            "Raging Bull",
            "Casablanca",
            "Citizen Kane",
            "Gone with the Wind",
            "The Wizard of Oz",
            "Gone With the Wind",
            "Lawrence of Arabia",
            "Vertigo",
            "Psycho",
            "Pulp Fiction",
            "Fight Club",
            "The Dark Knight",
            "Inception",
            "The Matrix",
            "Forrest Gump",
            "Goodfellas",
            "The Silence of the Lambs",
            "The Lord of the Rings: The Fellowship of the Ring",
            "The Lord of the Rings: The Two Towers",
            "The Lord of the Rings: The Return of the King",
            "The Avengers",
            "Interstellar",
            "The Departed",
            "Gladiator",
            "The Lion King",
            "Titanic",
            "Avatar",
            "The Green Mile",
            "The Prestige",
            "The Social Network",
            "The Grand Budapest Hotel",
            "Whiplash",
            "La La Land",
            "The Revenant",
            "Birdman",
            "The Shape of Water",
            "Moonlight",
            "Get Out",
            "Black Panther",
            "The Big Lebowski",
            "Eternal Sunshine of the Spotless Mind",
            "No Country for Old Men",
            "The Departed",
            "The Wolf of Wall Street",
            "Her",
            "Drive",
            "Mad Max: Fury Road",
            "The Martian",
            "Arrival",
            "Blade Runner 2049",
            "Baby Driver",
            "Dunkirk",
            "Three Billboards Outside Ebbing, Missouri",
            "Call Me by Your Name",
            "Lady Bird",
            "A Star Is Born",
            "Bohemian Rhapsody",
            "Joker",
            "Parasite",
            "1917",
            "Once Upon a Time in Hollywood",
            "Knives Out",
            "Tenet",
            "Soul",
            "Nomadland",
            "Minari",
            "Promising Young Woman",
            "Sound of Metal",
            "The Trial of the Chicago 7",
            "Mank",
            "Judas and the Black Messiah",
            "The Father",
            "Ma Rainey's Black Bottom",
            "One Night in Miami",
            "The Irishman",
            "Marriage Story",
            "Jojo Rabbit",
            "Little Women",
            "Ford v Ferrari",
            "1917",
            "The Farewell",
            "Uncut Gems",
            "The Lighthouse",
            "Portrait of a Lady on Fire",
            "Parasite",
            "Pain and Glory",
            "Marriage Story",
            "Joker",
            "Jojo Rabbit",
            "Ford v Ferrari",
            "1917",
            "Once Upon a Time in Hollywood",
            "Little Women",
            "The Irishman",
            "Parasite"
        };

        public (List<Movie> movies, List<Screen> screens, List<MovieScreening> screenings, List<Ticket> tickets) GenerateData()
        {
            var movies = new Faker<Movie>()
                .RuleFor(m => m.Id, f => 0)
                .RuleFor(m => m.Title, f => f.PickRandom(movieTitles))
                .RuleFor(m => m.Genre, f => f.PickRandom("Action", "Comedy", "Drama", "Horror", "Sci-Fi", "Adventure"))
                .RuleFor(m => m.ReleaseDate, f => f.Date.BetweenDateOnly(
                    DateOnly.FromDateTime(DateTime.Now.AddMonths(-3)),
                    DateOnly.FromDateTime(DateTime.Now)))
                .RuleFor(m => m.ReviewScore, f => f.Random.Double(1, 5))
                .RuleFor(m => m.BoardRating, f => f.PickRandom<Movie.Rating>())
                .Generate(10);

            var screenIndex = 1;
            var screens = new Faker<Screen>()
                .RuleFor(s => s.Id, f => 0)
                .RuleFor(s => s.Name, f => $"Screen {screenIndex++}")
                .RuleFor(s => s.Capacity, f => f.Random.Number(50, 500))
                .Generate(10);

            int screeningScreenIndex = 0;
            DateTimeOffset screeningTime = DateTimeOffset.UtcNow.AddHours(8).ToOffset(TimeSpan.Zero);
            var screenings = new Faker<MovieScreening>()
                .RuleFor(s => s.Id, f => 0)
                .RuleFor(s => s.Movie, f => f.PickRandom(movies))
                .RuleFor(s => s.Screen, f =>
                {
                    if (screeningScreenIndex == screens.Count - 1)
                    {
                        screeningScreenIndex = 0;
                    }
                    else
                    {
                        screeningScreenIndex++;
                    }

                    return screens[screeningScreenIndex];
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
                .RuleFor(s => s.TicketPrice, f => Math.Round(f.Random.Decimal(5, 20), 2))
                .Generate(50);

            var tickets = new Faker<Ticket>()
                .RuleFor(t => t.Id, f => 0)
                .RuleFor(t => t.MovieScreening, f => f.PickRandom(screenings))
                .RuleFor(t => t.SeatNumber, f => f.Random.Number(1, 50))
                .Generate(100);

            return (movies, screens, screenings, tickets);
        }
    }
}
