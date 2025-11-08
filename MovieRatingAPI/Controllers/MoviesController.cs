using Microsoft.AspNetCore.Mvc;
using MovieRatingAPI.DTOs;
using MovieRatingAPI.Interfaces;

namespace MovieRatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
        {
            var movies = await _movieService.GetAllAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovie(long id)
        {
            var movie = await _movieService.GetByIdAsync(id);

            if(movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieReadDTO>> CreateMovie(MovieCreateDTO movie)
        {
            var createdMovie = await _movieService.CreateAsync(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = createdMovie.Id }, createdMovie);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MovieReadDTO>> UpdateMovie(long id, MovieUpdateDTO movie)
        {
            var updatedMovie = await _movieService.UpdateAsync(id, movie);
            if(updatedMovie == null)
            {
                return NotFound();
            }

            return Ok(updatedMovie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(long id)
        {
            var result = await _movieService.DeleteAsync(id);
            if(!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("toprated")]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetTopRated()
        {
            var movies = await _movieService.GetTopRatedAsync();
            return Ok(movies);
        }
    }
}
