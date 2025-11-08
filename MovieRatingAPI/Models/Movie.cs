using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MovieRatingAPI.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Rating { get; set; }

        public string? Secret { get; set; }
    }
}