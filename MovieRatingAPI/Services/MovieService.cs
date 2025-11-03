using Microsoft.AspNetCore.Http.HttpResults;
using MovieRatingAPI.DTOs;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Models;
using System.Security.Policy;

namespace MovieRatingAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<MovieReadDTO?> GetByIdAsync(long id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if(movie == null)
            {
                return null;
            }
            return MovieToDTO(movie);
        }

        public async Task<MovieReadDTO> CreateAsync(MovieCreateDTO movieDTO)
        {
            var movie = new Movie
            {
                Title = movieDTO.Title,
                Rating = movieDTO.Rating
            };
            await _movieRepository.AddAsync(movie);
            await _movieRepository.SaveChangesAsync();

            return new MovieReadDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating
            };
        }

        public async Task<IEnumerable<MovieReadDTO>> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return MoviesToDTOS(movies);
        }

        public async Task<MovieReadDTO?> UpdateAsync(long id, MovieUpdateDTO movieDTO)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if(movie == null)
            {
                return null;
            }

            movie.Title = movieDTO.Title;
            movie.Rating = movieDTO.Rating;

            await _movieRepository.UpdateAsync(movie);

            return MovieToDTO(movie);

        }

        public async Task<bool> DeleteAsync(long id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if(movie == null)
            {
                return false;
            }   

            await _movieRepository.DeleteAsync(id);
            await _movieRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MovieReadDTO>> GetTopRatedAsync()
        {
            var movies = await _movieRepository.GetTopRatedAsync();
            return MoviesToDTOS(movies);
        }

        private static MovieReadDTO MovieToDTO(Movie movie) => new MovieReadDTO
        {
            Id = movie.Id,
            Title = movie.Title,
            Rating = movie.Rating
        };

        private static IEnumerable<MovieReadDTO> MoviesToDTOS(IEnumerable<Movie> movies)
        {
            return movies.Select(movie => new MovieReadDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating
            });
        }
    }
}
