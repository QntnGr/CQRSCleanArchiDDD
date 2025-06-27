using Application.Common.Interfaces.Services;
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


        [HttpGet("/GetById/{placeId}")]
        public async Task<IActionResult> GetAllPlaces(string placeId)
        {
            _logger.LogInformation("Getting last 5 reviews by place Id from google place API");
            var result = await _reviewService.GetReviewsById(placeId);
            return Ok(result);
        }
    }
}
