using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRatingAPI.DTOs;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Models;

namespace MovieRatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<ActionResult<ReviewReadDTO>> Create(ReviewCreateDTO reviewDto)
        {   
            var review = await _reviewService.CreateReviewAsync(reviewDto);
            return CreatedAtAction(nameof(GetReviewsByMovieId), 
                new { movieId = review.MovieID },
                review);
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<IEnumerable<ReviewReadDTO>>> GetReviewsByMovieId(long movieId)
        {
            var reviews = await _reviewService.GetReviewsByMovieIdAsync(movieId);
            return Ok(reviews); 
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult<ReviewReadDTO>> Update(long reviewId, ReviewUpdateDTO reviewUpdateDTO)
        {
            var review = await _reviewService.UpdateReviewAsync(reviewId, reviewUpdateDTO);
            return Ok(review);
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> Delete(long reviewId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);
            return NoContent();
        }
    }
}
