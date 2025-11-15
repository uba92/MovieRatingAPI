using MovieRatingAPI.Models;

namespace MovieRatingAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
    }
}
