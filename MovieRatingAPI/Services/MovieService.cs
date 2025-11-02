using MovieRatingAPI.DTOs;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Models;

namespace MovieRatingAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<MovieDTO?> GetByIdAsync(long id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if(movie == null)
            {
                return null;
            }
            return MovieToDTO(movie);
        }

        private static MovieDTO MovieToDTO(Movie movie) => new MovieDTO
        {
            Id = movie.Id,
            Title = movie.Title,
            Rating = movie.Rating
        };
    }
}
