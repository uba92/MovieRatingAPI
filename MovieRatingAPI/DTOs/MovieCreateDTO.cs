using System.ComponentModel.DataAnnotations;

namespace MovieRatingAPI.DTOs
{
    public class MovieCreateDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int Rating { get; set; }
    }
}
