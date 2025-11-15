using MovieRatingAPI.DTOs.Auth;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Models;

namespace MovieRatingAPI.Services
{
    public class AuthService : IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
