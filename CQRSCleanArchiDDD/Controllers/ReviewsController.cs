using Application.Common.Interfaces.Services;
using Application.Dto;
using Domain.Errors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CQRSCleanArchiDDD.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewsController(ILogger<ReviewsController> logger,
        IReviewService reviewService) 
        : ControllerBase
    {
        private readonly ILogger<ReviewsController> _logger = logger;
        private readonly IReviewService _reviewService = reviewService;


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
    }
}
