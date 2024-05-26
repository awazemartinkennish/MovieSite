using System.ComponentModel.DataAnnotations;

namespace MovieSite.Database.Models
{
    public class MovieScreening
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ScreenId { get; set; }

        [Required]
        public DateTimeOffset ScreeningTime { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal TicketPrice { get; set; }

        public Movie? Movie { get; set; }
        public Screen? Screen { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

    }
}
