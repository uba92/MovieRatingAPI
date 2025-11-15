using Microsoft.EntityFrameworkCore;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Models;

namespace MovieRatingAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MovieContext _context;

        public AuthRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync (string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync (string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
