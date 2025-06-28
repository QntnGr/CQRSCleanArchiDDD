using Application.Common.Interfaces.Persistance;
using Application.Dto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(PlaceDbContext placeDbContext)
        : base(placeDbContext)
    {
    }

    public async Task<List<Review>> GetAllReviewByPlaceAsync(Guid placeId)
    {
        return await Find(review => review.PlaceId == placeId).ToListAsync();
    }
}
