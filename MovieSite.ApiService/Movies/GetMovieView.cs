using System.Text.Json.Serialization;
using static MovieSite.Database.Models.Movie;

namespace MovieSite.ApiService.Movies
{
    public record GetMovieView
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Genre { get; init; }
        public DateOnly ReleaseDate { get; init; }
        public double ReviewScore { get; init; }
        public Rating BoardRatingInternal { private get; init; }
        public string BoardRating => BoardRatingInternal switch
        {
            Rating.UA => "UA",
            Rating.U => "U",
            Rating.PG => "PG",
            Rating.TwelveA => "12A",
            Rating.Fifteen => "15",
            Rating.Eighteen => "18",
            _ => ""
        };

        public List<ScreeningView> Screenings { get; init; }

        public record ScreeningView
        {
            public int ScreeningId { get; init; }
            public string Location { get; init; }
            public DateTimeOffset ScreeningTime { get; init; }
        }
    }
}
