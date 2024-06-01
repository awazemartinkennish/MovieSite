using MovieSite.Database.Models;

namespace MovieSite.ApiService.Movies
{
    public record GetMovieView
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public required string Genre { get; init; }
        public DateOnly ReleaseDate { get; init; }
        public double ReviewScore { get; init; }
        public Movie.Rating BoardRating { get; init; }

        public required List<ScreeningView> Screenings { get; init; }

        public record ScreeningView
        {
            public int ScreeningId { get; init; }
            public required string Location { get; init; }
            public DateTimeOffset ScreeningTime { get; init; }
        }
    }
}
