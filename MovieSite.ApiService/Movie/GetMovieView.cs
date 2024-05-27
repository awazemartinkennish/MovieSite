using static MovieSite.Database.Models.Movie;

namespace MovieSite.ApiService.Movie
{
    public record GetMovieView
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Genre { get; init; }
        public DateOnly ReleaseDate { get; init; }
        public double ReviewScore { get; init; }
        public Rating BoardRating { get; init; }
        public List<ScreeningView> Screenings { get; init; }

        public record ScreeningView
        {
            public int ScreeningId { get; init; }
            public string Location { get; init; }
            public DateTimeOffset ScreeningTime { get; init; }
        }
    }
}
