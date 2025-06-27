using Application.Common.Interfaces.Services;
using Application.Dto;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IApiServiceCall<ReviewsModel> _apiServiceCall;

    public ReviewService(IApiServiceCall<ReviewsModel> apiServiceCall)
    {
        _apiServiceCall = apiServiceCall;
        _apiServiceCall.AddQueryParameter("fields", "reviews");
        _apiServiceCall.AddQueryParameter("language", "fr");
    }

    public async Task<ReviewsModel> GetReviewsById(string placeId)
    {
        _apiServiceCall.AddQueryParameter("place_id", placeId);
        return await _apiServiceCall.GetAsync(placeId);
    }
}
