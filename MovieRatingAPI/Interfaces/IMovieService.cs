using MovieRatingAPI.DTOs;

namespace MovieRatingAPI.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetAllAsync();
        Task<MovieDTO?> GetByIdAsync(long id);
        Task<MovieDTO> CreateAsync(MovieDTO movieDTO);
        Task<MovieDTO?> UpdateAsync(long id, MovieDTO movieDTO);
        Task<bool> DeleteAsync(long id);

        Task<IEnumerable<MovieDTO>> GetTopRatedAsync();
    }
}
