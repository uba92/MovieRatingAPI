using Microsoft.EntityFrameworkCore;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Models;
namespace MovieRatingAPI.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieContext context) : base(context){ }

        public async Task<IEnumerable<Movie>> GetTopRatedAsync()
        {
            return await _context.Set<Movie>()
                .Where(m=> m.Rating >= 6)
                .OrderByDescending(m => m.Rating)
                .ToListAsync();
        }
    }
}
