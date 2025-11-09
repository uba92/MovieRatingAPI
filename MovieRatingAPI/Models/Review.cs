using System.ComponentModel.DataAnnotations;

namespace MovieRatingAPI.Models
{
    public class Review
    {
        public long Id { get; set; }
        public long MovieId { get; set; }
        [Required(ErrorMessage = "Comment is required.")]
        [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Movie? Movie { get; set; }
    }
}
