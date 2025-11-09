using MovieRatingAPI.DTOs;

namespace MovieRatingAPI.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewReadDTO>> GetReviewsByMovieIdAsync(long movieId);
        Task<ReviewReadDTO> CreateReviewAsync(ReviewCreateDTO reviewDTO);
        Task<ReviewReadDTO> UpdateReviewAsync(long movieid, ReviewUpdateDTO reviewUpdateDTO);
        Task<bool> DeleteReviewAsync(long reviewId);
    }
}
