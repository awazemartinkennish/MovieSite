using MovieSite.ApiService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieSite.Database
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<MovieScreening> MovieScreenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieScreening>().ToTable(nameof(MovieScreening))
                .HasOne(ms => ms.Movie);
            modelBuilder.Entity<MovieScreening>().ToTable(nameof(MovieScreening))
                .HasOne(ms => ms.Screen);
            modelBuilder.Entity<MovieScreening>().ToTable(nameof(MovieScreening))
                .HasMany(ms => ms.Tickets);
        }
    }
}
