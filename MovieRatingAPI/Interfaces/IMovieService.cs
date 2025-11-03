using MovieRatingAPI.DTOs;

namespace MovieRatingAPI.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieReadDTO>> GetAllAsync();
        Task<MovieReadDTO?> GetByIdAsync(long id);
        Task<MovieReadDTO> CreateAsync(MovieReadDTO movieDTO);
        Task<MovieReadDTO?> UpdateAsync(long id, MovieUpdateDTO movieDTO);
        Task<bool> DeleteAsync(long id);

        Task<IEnumerable<MovieReadDTO>> GetTopRatedAsync();
    }
}
