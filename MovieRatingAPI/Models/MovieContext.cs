using Microsoft.EntityFrameworkCore;

namespace MovieRatingAPI.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasOne(r => r.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
