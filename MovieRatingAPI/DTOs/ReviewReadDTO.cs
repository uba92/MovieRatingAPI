namespace MovieRatingAPI.DTOs
{
    public class ReviewReadDTO
    {
        public long Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } 
        public string? MovieTitle { get; set; }
    }
}
