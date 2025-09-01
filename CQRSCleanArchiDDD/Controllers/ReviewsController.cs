using Application.Common.Interfaces.Services;
using Application.Dto;
using Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRSCleanArchiDDD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
//[AllowAnonymous] //debug
public class ReviewsController(ILogger<ReviewsController> logger,
    IReviewService reviewService,
    IScraperService scraperService,
    IGoogleReviewParser googleReviewParser) 
    : ControllerBase
{
    private readonly ILogger<ReviewsController> _logger = logger;
    private readonly IReviewService _reviewService = reviewService;
    private readonly IScraperService _scraperService = scraperService;
    private readonly IGoogleReviewParser _googleReviewParser = googleReviewParser;


    [HttpGet("/GetAllByPlace/{placeId}")]
    public async Task<IActionResult> GetAllReviewsByPlace(string placeId)
    {
        _logger.LogInformation("Getting all reviews by place");
        var result = await _reviewService.GetReviewsByIdAsync(placeId);
        return Ok(result);
    }

    [HttpPost("/InsertOneByPlace/{placeId}")]
    public async Task<IActionResult> InsertReview(ReviewDto review, string placeId)
    {
        _logger.LogInformation("Insert one review by place");
        var result = await _reviewService.AddReview(review, placeId);
        return Ok(result);
    }

    [HttpPut("/SyncronyzeReviews/{placeId}")]
    public async Task<IActionResult> SyncronyzeReviews(string placeId)
    {
        _logger.LogInformation("Synchronize reviews by placeId and search string");
        var result = await _reviewService.SyncronizeReviewWithScrapperAsync(placeId);
        if (!result.Any()) {
            var error = DomainError.NotFound("No reviews found to synchronize");
            return NotFound(error);
        }
        return Ok(result);
    }
}
