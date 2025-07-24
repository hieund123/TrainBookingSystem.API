using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Models.DTOs.TrainStation;
using TrainBookingSystem.API.Services.JourneyStation;
using TrainBookingSystem.API.Services.TrainStation;

namespace TrainBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainStationController : ControllerBase
    {
        private readonly ITrainStationService _trainStationService;
        private readonly IJourneyStationService _journeyStationService;

        public TrainStationController(ITrainStationService trainStationService, IJourneyStationService journeyStationService)
        {
            _trainStationService = trainStationService;
            _journeyStationService = journeyStationService;
        }

        [HttpGet("GetAllStations")]
        public async Task<IActionResult> GetAllStations()
        {
            var stations = await _trainStationService.GetAllStationsAsync();
            return Ok(stations);
        }

        // GET: api/TrainStation/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var station = await _trainStationService.GetStationByIdAsync(id);
            if (station == null)
                return NotFound(new { message = "Không tìm thấy ga tàu." });

            return Ok(station);
        }


        [HttpPost]
        public async Task<IActionResult> CreateStation([FromBody] TrainStationCreateDTO dto)
        {
            try
            {
                var result = await _trainStationService.InsertStationAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStation(int id, [FromBody] TrainStationUpdateDTO dto)
        {
            var updated = await _trainStationService.UpdateStationAsync(id, dto);
            if (!updated) return NotFound(new { message = "Không tìm thấy ga cần cập nhật." });

            return Ok(new { message = "Cập nhật thành công." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id, [FromQuery] bool force = false)
        {
            try
            {
                var journeys = await _journeyStationService.GetJourneysHasStationAsync(id);
                if (journeys.Any() && !force)
                {
                    // Ga đang được dùng trong các hành trình và chưa có xác nhận xóa
                    return Conflict(new
                    {
                        message = "Ga đang được sử dụng trong các hành trình. Xác nhận xóa?",
                        requiresConfirmation = true,
                        journeys = journeys.Select(j => new { j.Id, j.Name })
                    });
                }

                var result = await _trainStationService.DeleteStationAsync(id);
                if (!result)
                    return NotFound(new { message = "Không tìm thấy ga." });

                return Ok(new { message = "Xóa ga thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", detail = ex.Message });
            }
        }

    }
}
