using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Models.DTOs.JourneyStation;
using TrainBookingSystem.API.Services.JourneyStation;

namespace TrainBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyStationController : ControllerBase
    {
        private readonly IJourneyStationService _journeyStationService;

        public JourneyStationController(IJourneyStationService journeyStationService)
        {
            _journeyStationService = journeyStationService;
        }

        [HttpGet("by-journey/{journeyId}")]
        public async Task<IActionResult> GetStationsByJourneyId(int journeyId)
        {
            var result = await _journeyStationService.GetStationsByJourneyIdAsync(journeyId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddJourneyStation([FromBody] JourneyStationCreateDto dto)
        {
            await _journeyStationService.AddJourneyStationAsync(dto);
            return Ok(new { message = "Thêm ga dừng thành công." });
        }
    }
}
