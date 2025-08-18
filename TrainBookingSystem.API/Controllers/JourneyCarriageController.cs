using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Services.JourneyCarriage;
using TrainBookingSystem.API.Models.DTOs.JourneyCarriage;

namespace TrainBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneyCarriageController : ControllerBase
    {
        private readonly IJourneyCarriageService _journeyCarriageService;

        public JourneyCarriageController(IJourneyCarriageService journeyCarriageService)
        {
            _journeyCarriageService = journeyCarriageService;
        }

        // GET: api/JourneyCarriage/GetJourneysHasCarriage/5
        [HttpGet("GetJourneysHasCarriage/{carriageClassId}")]
        public async Task<IActionResult> GetJourneysHasCarriage(int carriageClassId)
        {
            var result = await _journeyCarriageService.GetJourneysHasCarriageAsync(carriageClassId);
            return Ok(result);
        }

        [HttpPost("InsertJourneyCarriage")]
        public async Task<IActionResult> InsertJourneyCarriage([FromBody] JourneyCarriageCreateDTO dto)
        {
            var success = await _journeyCarriageService.InsertJourneyCarriageAsync(dto);
            if (!success)
            {
                return BadRequest(new { message = "Thêm toa vào hành trình thất bại." });
            }

            return Ok(new { message = "Đăng ký toa vào hành trình thành công." });
        }


        [HttpGet("GetCarriagesInJourney/{journeyId}")]
        public async Task<IActionResult> GetCarriagesInJourney(int journeyId)
        {
            var result = await _journeyCarriageService.GetCarriagesInJourneyAsync(journeyId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJourneyCarriage(int id)
        {
            var success = await _journeyCarriageService.DeleteJourneyCarriageAsync(id);
            if (!success)
                return BadRequest(new { message = "Xóa toa thất bại hoặc không tồn tại." });

            return Ok(new { message = "Xóa toa thành công." });
        }

    }
}
