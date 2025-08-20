using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Models.DTOs.CarriageClass;
using TrainBookingSystem.API.Services.CarriageClass;
using TrainBookingSystem.API.Services.JourneyCarriage;

namespace TrainBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarriageClassController : ControllerBase
    {
        private readonly ICarriageClassService _carriageClassService;
        private readonly IJourneyCarriageService _journeyCarriageService;

        public CarriageClassController(ICarriageClassService carriageClassService, IJourneyCarriageService journeyCarriageService)
        {
            _carriageClassService = carriageClassService;
            _journeyCarriageService = journeyCarriageService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCarriageClasses()
        {
            var result = await _carriageClassService.GetAllCarriageClassesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carriageClassService.GetCarriageClassByIdAsync(id);
            if (result == null)
                return NotFound(new { message = "Không tìm thấy loại toa." });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarriageClass([FromBody] CarriageClassCreateDTO dto)
        {
            var success = await _carriageClassService.InsertCarriageClassAsync(dto);
            if (!success)
                return StatusCode(500, new { message = "Thêm loại toa thất bại." });

            return Ok(new { message = "Thêm loại toa thành công." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarriageClass(int id, [FromQuery] bool force = false)
        {
            var journeysUsingCarriage = await _journeyCarriageService.GetJourneysHasCarriageAsync(id);

            if (journeysUsingCarriage.Any() && !force)
            {
                return BadRequest(new
                {
                    message = "Không thể xóa loại toa vì đang được sử dụng trong các hành trình.",
                    journeys = journeysUsingCarriage
                });
            }

            // Nếu force = true có thể xóa 

            var result = await _carriageClassService.DeleteCarriageClassAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy loại toa cần xóa." });
            }

            return Ok(new { message = "Xóa loại toa thành công." });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarriageClass(int id, [FromBody] CarriageClassUpdateDTO dto)
        {
            var success = await _carriageClassService.UpdateCarriageClassAsync(id, dto);
            if (!success)
                return NotFound(new { message = "Không tìm thấy loại toa cần sửa." });

            return Ok(new { message = "Cập nhật loại toa thành công." });
        }
    }
}
