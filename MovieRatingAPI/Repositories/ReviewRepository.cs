using Microsoft.EntityFrameworkCore;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Models;

namespace MovieRatingAPI.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieContext context) : base(context) { }

        public async Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(long movieId)
        {
            return await _context.Set<Review>()
                .Where(r => r.MovieId == movieId)
                .ToListAsync();
        }
    }
}
