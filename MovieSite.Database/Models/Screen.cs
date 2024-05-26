using System.ComponentModel.DataAnnotations;


namespace MovieSite.Database.Models
{
    public class Screen
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, 500)]
        public int Capacity { get; set; }

        public ICollection<MovieScreening>? Screenings { get; set; }
    }
}
