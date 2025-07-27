using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Models.DTOs.TrainJourney;
using TrainBookingSystem.API.Services.TrainJourney;

namespace TrainBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainJourneyController : ControllerBase
    {
        private readonly ITrainJourneyService _trainJourneyService;

        public TrainJourneyController(ITrainJourneyService service)
        {
            _trainJourneyService = service;
        }

        // GET: api/TrainJourney
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var journeys = await _trainJourneyService.GetAllJourneysAsync();
                return Ok(journeys);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        // GET: api/TrainJourney/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var journey = await _trainJourneyService.GetJourneyByIdAsync(id);
                if (journey == null)
                    return NotFound(new { message = "Không tìm thấy hành trình." });

                return Ok(journey); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        // POST: api/TrainJourney
        [HttpPost]
        public async Task<IActionResult> CreateJourney([FromBody] TrainJourneyCreateDTO dto)
        {
            try
            {
                var result = await _trainJourneyService.InsertJourneyAsync(dto);
                return Ok(result); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchJourneys([FromQuery] DateTime? departureDate, [FromQuery] int? startStationId, [FromQuery] int? endStationId)
        {
            var result = await _trainJourneyService.SearchJourneys(departureDate, startStationId, endStationId);
            return Ok(result);
        }

    }
}
