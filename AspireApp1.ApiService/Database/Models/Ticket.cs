using System.ComponentModel.DataAnnotations;

namespace AspireApp1.ApiService.Database.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public int MovieScreeningId { get; set; }

        [Required]
        public int SeatNumber { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }

        public MovieScreening? MovieScreening { get; set; }
    }
}
