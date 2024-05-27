using MovieSite.Database.Models;

namespace MovieSite.ApiService.MovieScreenings
{
    public class GetMovieScreeningView
    {
        public int Id { get; init; }
        public required string MovieTitle { get; init; } 
        public required string Location { get; init; }
        public DateTimeOffset StartTime { get; init; }
        public int SeatsSold { get; init; }
        public int Capacity { get; init; }
        public decimal PercentageAvailable => Math.Round(((Capacity - SeatsSold) * 100m) / Capacity, 2);
        public decimal TicketPrice { get; init; }
        public double ReviewScore { get; init; }
        public Movie.Rating BoardRating { get; init; }
    }
}
