using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRSCleanArchiDDD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceManagementController(ILogger<PlaceManagementController> logger, IPlaceRepository placeRepository) : ControllerBase
    {
        private readonly IPlaceRepository _placeRepository = placeRepository;
        private readonly ILogger<PlaceManagementController> _logger = logger;

        [HttpGet("/GetAll")]
        public async Task<IActionResult> GetAllPlaces()
        {
            _logger.LogInformation("Getting all Places");
            var result = await _placeRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpPost("/AddOne")]
        public async Task<IActionResult> Add(Place place)
        {
            _logger.LogInformation("New place will be added for placeId: {0}", place.PlaceId);
            await _placeRepository.AddAsync(place);
            return Ok();
        }
    }
}
