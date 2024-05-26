using System.ComponentModel.DataAnnotations;

namespace MovieSite.ApiService.Database.Models
{
    public class Movie
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }
        [Required]
        public DateOnly ReleaseDate { get; set; }
        [Required]
        public double ReviewScore { get; set; }
        [Required]
        public Rating BoardRating { get; set; }


        public enum Rating
        {
            UA = 0,
            U = 1,
            PG = 2,
            TwelveA = 3,
            Fifteen = 4,
            Eighteen = 5
        }

        public ICollection<MovieScreening> Screenings { get; set; }
    }
}
