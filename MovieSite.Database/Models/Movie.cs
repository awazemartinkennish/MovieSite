using System.ComponentModel.DataAnnotations;

namespace MovieSite.Database.Models
{
    public class Movie
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Genre { get; set; }
        [Required]
        public DateOnly ReleaseDate { get; set; }
        [Required]
        [Range(0, 5)]
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

        public ICollection<MovieScreening>? Screenings { get; set; }
    }
}
