

using Application.Dto;

namespace Application.Common.Interfaces.Services;

public interface IReviewService
{
    Task<ReviewsModel> GetReviewsById(string placeId);
}
