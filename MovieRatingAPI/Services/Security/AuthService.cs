using Microsoft.IdentityModel.Tokens;
using MovieRatingAPI.DTOs.Auth;
using MovieRatingAPI.Exceptions;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Interfaces.Security;
using MovieRatingAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieRatingAPI.Services.Security
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher, IConfiguration configuration)
        { 
            _authRepository =  authRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
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

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
