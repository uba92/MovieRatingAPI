namespace MovieRatingAPI.DTOs.Auth
{
    public class RegisterResponse
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
