
using Application.Common.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

public class GoogleReviewParserServiceTest
{
    private readonly IGoogleReviewParser _googleReviewParser;

    public GoogleReviewParserServiceTest()
    {
        //setup
        var mockLogger = new Mock<ILogger<GoogleReviewParser>>();
        _googleReviewParser = new GoogleReviewParser(mockLogger.Object);
    }

    [Fact]
    public void ParseHtmlTest()
    {
        //arrange
        var html = File.ReadAllText(@"assets/htmlGoogleReviews.html");

        //act
        var reviews = _googleReviewParser.ParseReviews(html);

        //verify
        Assert.Equal(10, reviews.Count);
        Assert.All(reviews, review =>
        {
            Assert.True(review.Rating != 0);
            Assert.True(!string.IsNullOrEmpty(review.AuthorName));
            Assert.True(review.Date != DateTimeOffset.MinValue);
        });
    }
}