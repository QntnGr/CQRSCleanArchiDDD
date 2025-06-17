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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
            })
            .ToArray();
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
