using Application.Common.Interfaces.Services;
using Application.Dto;
using Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRSCleanArchiDDD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ReviewsController(ILogger<ReviewsController> logger,
    IReviewService reviewService,
    IScraperService scraperService) 
    : ControllerBase
{
    private readonly ILogger<ReviewsController> _logger = logger;
    private readonly IReviewService _reviewService = reviewService;
    private readonly IScraperService _scraperService = scraperService;


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
        _logger.LogInformation("Insert one review by place");
        var result = await _reviewService.SyncronizeReviewFromGoogleApiById(placeId);
        if (!result.Any()) {
            var error = DomainError.NotFound("No reviews found for the specified place ID.");
            return NotFound(error);
        }
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPut("/GetHtml/{endPoint}")]
    public async Task<IActionResult> GetHtml(string endPoint)
    {
        _logger.LogInformation("Insert one review by place");
        var result = await _scraperService.GetHtmlAsync(endPoint);
        if (!result.Any()) {
            var error = DomainError.NotFound("No reviews found for the specified endPoint.");
            return NotFound(error);
        }
        return Ok(result);
    }
}
