

using Application.Dto;

namespace Application.Common.Interfaces.Services;

public interface IReviewService
{
    Task<List<ReviewDto>> GetReviewsByIdAsync(string placeId);
    Task<List<ReviewDto>> AddReview(ReviewDto reviewDto, string placeId);
    Task<List<ReviewDto>> SyncronizeReviewFromGoogleApiById(string placeId);
}
