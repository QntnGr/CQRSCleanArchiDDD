using Application.Common.Interfaces.Persistance;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(PlaceDbContext placeDbContext)
        : base(placeDbContext)
    {
    }
}
