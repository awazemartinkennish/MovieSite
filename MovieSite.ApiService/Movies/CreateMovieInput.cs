﻿using System.ComponentModel.DataAnnotations;

namespace MovieSite.ApiService.Movies
{
    public record CreateMovieInput
    {
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
        [AllowedValues("UA", "U", "PG", "12A", "15", "18")]
        public required string BoardRating { get; set; }

    }
}
