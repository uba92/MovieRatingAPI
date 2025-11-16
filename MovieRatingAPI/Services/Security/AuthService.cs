using MovieRatingAPI.DTOs.Auth;
using MovieRatingAPI.Exceptions;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Interfaces.Security;
using MovieRatingAPI.Models;

namespace MovieRatingAPI.Services.Security
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher)
        { 
            _authRepository =  authRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            string normalizedEmail = request.Email.Trim().ToLower();
            if(await _authRepository.EmailExistsAsync(normalizedEmail))
            {
                throw new EmailAlreadyExistsException($"Email: {normalizedEmail} already registered!");
            }
            string hashedPassword = _passwordHasher.HashPassword(request.Password);

            var user = new User
            {
                Email = normalizedEmail,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _authRepository.CreateAsync(user);

            return new RegisterResponse
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                CreatedAt = createdUser.CreatedAt
            };
        }
        public Task<LoginResponse> LoginAsync(LoginRequest request)
        {

            throw new NotImplementedException();
        }
    }
}
