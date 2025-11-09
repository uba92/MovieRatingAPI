using System.ComponentModel.DataAnnotations;

namespace MovieRatingAPI.DTOs
{
    public class ReviewUpdateDTO
    {
        [Required(ErrorMessage = "Comment is required.")]
        [MinLength(5, ErrorMessage = "Comment must be at least 5 characters long.")]
        [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
        public string Comment { get; set; } = string.Empty;
    }
}
