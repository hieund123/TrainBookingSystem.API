using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Services.TrainStation;

namespace TrainBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainStationController : ControllerBase
    {
        private readonly ITrainStationService _trainStationService;

        public TrainStationController(ITrainStationService trainStationService)
        {
            _trainStationService = trainStationService;
        }

        [HttpGet("GetAllStations")]
        public async Task<IActionResult> GetAllStations()
        {
            var stations = await _trainStationService.GetAllStationsAsync();
            return Ok(stations);
        }
    }
}
