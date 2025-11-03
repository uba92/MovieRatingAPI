using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRatingAPI.DTOs;
using MovieRatingAPI.Models;

namespace MovieRatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
        {
            return await _context.Movies.Select(x => MovieToDTO(x)).ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovie(long id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return MovieToDTO(movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(long id, MovieReadDTO movieDto)
        {
            if (id != movieDto.Id)
            {
                return BadRequest();
            }

            var movie = await _context.Movies.FindAsync(id);
            if(movie == null)
            {
                return NotFound();
            }

            movie.Title = movieDto.Title;
            movie.Rating = movieDto.Rating;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MovieExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieReadDTO>> PostMovie(MovieReadDTO movieDto)
        {

            var movie = new Movie
            {
                Title = movieDto.Title,
                Rating = movieDto.Rating
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, MovieToDTO(movie));
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(long id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Converts a Movie to a MovieDTO
        private static MovieReadDTO MovieToDTO(Movie movie) =>
            new MovieReadDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating
            };

        private bool MovieExists(long id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
