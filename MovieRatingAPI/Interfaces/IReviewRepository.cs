using MovieRatingAPI.Models;

namespace MovieRatingAPI.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(long movieId);
    }
}
