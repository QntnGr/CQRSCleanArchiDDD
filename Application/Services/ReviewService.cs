using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Dto;
using Domain.Entities;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IApiServiceCall<ReviewsModel> _apiServiceCall;
    private readonly IPlaceRepository _placeRepository;
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IApiServiceCall<ReviewsModel> apiServiceCall,
        IPlaceRepository placeRepository,
        IReviewRepository reviewRepository)
    {
        _apiServiceCall = apiServiceCall;
        _apiServiceCall.AddQueryParameter("fields", "reviews");
        _apiServiceCall.AddQueryParameter("language", "fr");
        _placeRepository = placeRepository;
        _reviewRepository = reviewRepository;
    }

    //public async Task<ReviewsModel> GetReviewsById(string placeId)
    //{
    //    _apiServiceCall.AddQueryParameter("place_id", placeId);
    //    return await _apiServiceCall.GetAsync(placeId);
    //}
    /// <summary>
    /// insert missing reviews linked to place and return all reviews from place
    /// </summary>
    /// <param name="placeId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<ReviewDto>> SyncronizeReviewFromGoogleApiById(string placeId)
    {
        _apiServiceCall.AddQueryParameter("place_id", placeId);
        var reviewsModel = await _apiServiceCall.GetAsync();
        if (reviewsModel.result == null)
        {
            return new();
        }

        var reviews = reviewsModel.result.reviews.Select(review => review.ToEntity());
        var place = await _placeRepository.FindOneAsync(p => p.PlaceId == placeId);
        if (place == null)
        {
            throw new Exception("Place not found");
        }
        var reviewsOld = await _reviewRepository.GetAllReviewByPlaceAsync(place.Id);
        reviews = AssignAndComparePlaceIdToReviews(place, reviews, reviewsOld);

        await _reviewRepository.AddManyAsync(reviews);

        return await GetReviewsByIdAsync(placeId);
    }

    public async Task<List<ReviewDto>> GetReviewsByIdAsync(string placeId)
    {
        var place = await _placeRepository.FindOneAsync(p => p.PlaceId == placeId);
        if (place == null)
        {
            throw new Exception("Place not found");
        }
        var reviews = await _reviewRepository.GetAllReviewByPlaceAsync(place.Id);
        return reviews.Select(review => ReviewDto.FromEntity(review)).ToList();
    }

    public async Task<List<ReviewDto>> AddReview(ReviewDto reviewDto, string placeId)
    {
        var place = await _placeRepository.FindOneAsync(p => p.PlaceId == placeId);
        if (place == null)
        {
            throw new Exception("Place not found");
        }
        var review = reviewDto.ToEntity();
        review.Place = place;
        await _reviewRepository.AddAsync(review);

        return await GetReviewsByIdAsync(placeId);
    }

    #region private
    private static IEnumerable<Review> AssignAndComparePlaceIdToReviews(Place place, IEnumerable<Review> reviews, List<Review> reviewsOld)
    {
        var filtered = reviews.Where(review => !reviewsOld.Select(ro => ro.Date).Contains(review.Date)).ToList();
        foreach (var review in filtered)
        {
            review.Place = place;
        }
        return filtered;
    }
    #endregion
}
