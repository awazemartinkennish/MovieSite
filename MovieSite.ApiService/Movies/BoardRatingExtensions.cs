using MovieSite.Database.Models;

namespace MovieSite.ApiService.Movies
{
    public static class BoardRatingExtensions
    {
        public static string ToPrettyString(this Movie.Rating rating) => rating switch
        {
            Movie.Rating.UA => "UA",
            Movie.Rating.U => "U",
            Movie.Rating.PG => "PG",
            Movie.Rating.TwelveA => "12A",
            Movie.Rating.Fifteen => "15",
            Movie.Rating.Eighteen => "18",
            _ => "Unknown"
        };
        public static Movie.Rating ToRating(this string rating) => rating switch
        {
            "UA" => Movie.Rating.UA,
            "U" => Movie.Rating.U,
            "PG" => Movie.Rating.PG,
            "12A" => Movie.Rating.TwelveA,
            "15" => Movie.Rating.Fifteen,
            "18" => Movie.Rating.Eighteen,
            _ => Movie.Rating.UA
        };
    }
}
