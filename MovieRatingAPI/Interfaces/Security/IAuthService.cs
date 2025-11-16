using MovieRatingAPI.DTOs.Auth;

namespace MovieRatingAPI.Interfaces.Security
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
