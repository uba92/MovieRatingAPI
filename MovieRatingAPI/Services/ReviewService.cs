using MovieRatingAPI.DTOs;
using MovieRatingAPI.Exceptions;
using MovieRatingAPI.Interfaces;
using MovieRatingAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieRatingAPI.Services
{
    public class ReviewService : IReviewService
    {
        public readonly IMovieRepository _movieRepository;
        public readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository, IMovieRepository movieRepository) 
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }
        public async Task<ReviewReadDTO> CreateReviewAsync(ReviewCreateDTO reviewDTO)
        {
            if (string.IsNullOrWhiteSpace(reviewDTO.Comment))
            {
                throw new ValidationException("Comment cannot be empty.");
            }
                var review = new Review
            {
                MovieId = reviewDTO.MovieId,
                Comment = reviewDTO.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();

            return ReviewToDTO(review);
        }

        public async Task<bool> DeleteReviewAsync(long reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null)
            {
                throw new NotFoundException($"Review with id {reviewId} not found.");
            }

            await _reviewRepository.DeleteAsync(reviewId);
            await _reviewRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ReviewReadDTO>> GetReviewsByMovieIdAsync(long movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            if (movie == null)
            {
                throw new NotFoundException($"Movie with id {movieId} not found.");
            }

            var reviews = await _reviewRepository.GetReviewsByMovieIdAsync(movieId);
            return ReviewsToDTOs(reviews); 
            
        }

        public async Task<ReviewReadDTO> UpdateReviewAsync(long reviewId, ReviewUpdateDTO reviewUpdateDTO)
        {
            if(string.IsNullOrWhiteSpace(reviewUpdateDTO.Comment))
            {
                throw new ValidationException("Comment cannot be empty.");
            }
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null)
            {
                throw new NotFoundException($"Review with id {reviewId} not found.");
            }
            review.Comment = reviewUpdateDTO.Comment;
            review.UpdatedAt = DateTime.UtcNow;
            await _reviewRepository.UpdateAsync(review);
            await _reviewRepository.SaveChangesAsync();
            return ReviewToDTO(review);
        }

        private static ReviewReadDTO ReviewToDTO(Review review) => new ReviewReadDTO
        {
            Id = review.Id,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt,
            UpdatedAt = review.UpdatedAt,
            MovieTitle = review.Movie?.Title
        };

        private static IEnumerable<ReviewReadDTO> ReviewsToDTOs(IEnumerable<Review> reviews)
        {
            return reviews.Select(review => new ReviewReadDTO
            {
                Id = review.Id,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                UpdatedAt = review.UpdatedAt,
                MovieTitle = review.Movie?.Title
            });
        }
    }
}
