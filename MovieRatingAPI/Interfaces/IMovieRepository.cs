using MovieRatingAPI.Models;

namespace MovieRatingAPI.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTopRatedAsync();
    }
}
