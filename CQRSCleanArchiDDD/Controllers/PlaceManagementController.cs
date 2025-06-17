using Application.Common.Interfaces.Services;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CQRSCleanArchiDDD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceManagementController(
        ILogger<PlaceManagementController> logger,
        IPlaceService placeService) 
        : ControllerBase
    {
        private readonly IPlaceService _placeService = placeService;
        private readonly ILogger<PlaceManagementController> _logger = logger;

        [HttpGet("/GetAll")]
        public async Task<IActionResult> GetAllPlaces()
        {
            _logger.LogInformation("Getting all Places");
            var result = await _placeService.GetAllPlacesAsync();
            return Ok(result);
        }

        [HttpPost("/AddOne")]
        public async Task<IActionResult> Add(PlaceDto place)
        {
            _logger.LogInformation("New place will be added for placeId: {0}", place.PlaceId);
            await _placeService.AddOnePlaceAsync(place);
            return Ok();
        }
    }
}
