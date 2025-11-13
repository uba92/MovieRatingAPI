namespace MovieRatingAPI.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
