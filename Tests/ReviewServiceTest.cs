using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Services;
using Application.Dto;
using Application.Services;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Moq;

namespace Tests;

public class ReviewServiceTest
{
    private readonly IReviewService _reviewService;

    public ReviewServiceTest()
    {
        var mockApiSericeCall = new Mock<IApiServiceCall<ReviewsModel>>();
        var mockPlaceRepository = new Mock<IPlaceRepository>();
        var mockReviewRepository = new Mock<IReviewRepository>();
        var mockScrapperService = new Mock<IScraperService>();
        var mockGoogleReviewParser = new Mock<IGoogleReviewParser>();

        _reviewService = new ReviewService(mockApiSericeCall.Object,
            mockPlaceRepository.Object,
            mockReviewRepository.Object,
            mockScrapperService.Object,
            mockGoogleReviewParser.Object);
    }

    [Fact]
    public void ComparReviewsShouldReturnEmptyWhenObjectHaveSameContrbutor()
    {
        var place = GetPlaceEntityTest();
        var oldReviews = GetReviewsFromDBTest();
        var reviews = GetReviewsFromScrapperTest();

        var reviewsResult = _reviewService.AssignAndComparePlaceIdToReviews(place, reviews, oldReviews);

        Assert.Empty(reviewsResult);
    }

    private Place GetPlaceEntityTest()
    {
        return new Place()
        {
            Id = Guid.NewGuid(),
            Description = "test place",
            Name = "test",
            PlaceId = "TESTID"
        };
    }

    private List<Review> GetReviewsFromDBTest()
    {
        return new List<Review>
        {
            new Review
            {
                AuthorName = "contributor1",
                ProfilePhotoUrl = "giguigu",
                Rating = 5,
                Place = null,
                Date = DateTimeOffset.MinValue,
            },
            new Review
            {
                AuthorName = "contributor2",
                ProfilePhotoUrl = "giguigu",
                Rating = 5,
                Place = null,
                Date = DateTimeOffset.MaxValue,
            },
            new Review
            {
                AuthorName = "contributor3",
                ProfilePhotoUrl = "giguigu",
                Rating = 4,
                Place = null,
                Date = DateTimeOffset.MaxValue,
            }
        };
    }

    private List<Review> GetReviewsFromScrapperTest()
    {
        return new List<Review>
        {
            new Review
            {
                AuthorName = "contributor1",
                ProfilePhotoUrl = "kizgfiezgf",
                Rating = 4,
                Place = null,
                Date = DateTimeOffset.MinValue,
            },
            new Review
            {
                AuthorName = "contributor2",
                ProfilePhotoUrl = "kzjdoéiyed",
                Rating = 3,
                Place = null,
                Date = DateTimeOffset.MaxValue,
            },
            new Review
            {
                AuthorName = "contributor3",
                ProfilePhotoUrl = "ajsgfduyt",
                Rating = 2,
                Place = null,
                Date = DateTimeOffset.MaxValue,
            }
        };
    }
}