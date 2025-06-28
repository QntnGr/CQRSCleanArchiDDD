
using Domain.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IReviewRepository : IRepository<Review>
{
    Task<List<Review>> GetAllReviewByPlaceAsync(Guid placeId);
}
