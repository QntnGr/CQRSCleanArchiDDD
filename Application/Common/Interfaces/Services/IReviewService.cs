

using Application.Dto;
using Domain.Entities;

namespace Application.Common.Interfaces.Services;

public interface IReviewService
{
    Task<List<ReviewDto>> GetReviewsByIdAsync(string placeId);
    Task<List<ReviewDto>> AddReview(ReviewDto reviewDto, string placeId);
    Task<List<ReviewDto>> SyncronizeReviewFromGoogleApiById(string placeId);
    Task<List<ReviewDto>> SyncronizeReviewWithScrapperAsync(string placeId);
    IEnumerable<Review> AssignAndComparePlaceIdToReviews(Place place, IEnumerable<Review> reviews, List<Review> reviewsOld);
}
