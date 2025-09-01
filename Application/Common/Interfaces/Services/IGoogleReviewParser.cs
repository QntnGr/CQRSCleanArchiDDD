
using Domain.Entities;

namespace Application.Common.Interfaces.Services;

public interface IGoogleReviewParser
{
    List<Review> ParseReviews(string html);
}
