using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MovieRatingAPI.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Author { get; set; }
        public int Year { get; set; }
    }
}
