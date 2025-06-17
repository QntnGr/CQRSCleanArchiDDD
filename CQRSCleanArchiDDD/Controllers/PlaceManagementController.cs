using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CQRSCleanArchiDDD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceManagementController(ILogger<PlaceManagementController> logger, IPlaceRepository placeRepository) : ControllerBase
    {
        private readonly IPlaceRepository _placeRepository = placeRepository;
        private readonly ILogger<PlaceManagementController> _logger = logger;

        [HttpGet(Name = "GetPlaces")]
        public IEnumerable<Place> GetAllPlaces()
        {
            _logger.LogInformation("Getting all Places");
            return _placeRepository.GetAll().ToArray();
        }

        [HttpPost(Name = "AddPlace")]
        public bool Add(Place place)
        {
            _logger.LogInformation("New place will be added for placeId: {0}", place.PlaceId);
            _placeRepository.Add(place);
            return true;
        }
    }
}
