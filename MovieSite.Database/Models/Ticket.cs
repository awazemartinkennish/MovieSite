using System.ComponentModel.DataAnnotations;

namespace MovieSite.Database.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public int MovieScreeningId { get; set; }

        [Required]
        public int SeatNumber { get; set; }


        public MovieScreening? MovieScreening { get; set; }
    }
}
