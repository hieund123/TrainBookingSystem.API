using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Models.DTOs.CarriagePrice;
using TrainBookingSystem.API.Services.CarriagePrice;

namespace TrainBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarriagePriceController : ControllerBase
    {
        private readonly ICarriagePriceService _carriagePriceService;

        public CarriagePriceController(ICarriagePriceService carriagePriceService)
        {
            _carriagePriceService = carriagePriceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarriagePrices()
        {
            var prices = await _carriagePriceService.GetAllCarriagePricesAsync();
            return Ok(prices);
        }

        [HttpGet("{scheduleId}/{carriageClassId}")]
        public async Task<ActionResult<CarriagePriceReadDto>> GetCarriagePriceById(int scheduleId, int carriageClassId)
        {
            var dto = await _carriagePriceService.GetCarriagePriceByIdAsync(scheduleId, carriageClassId);
            if (dto == null)
                return NotFound("Không tìm thấy giá cho lịch trình và loại toa này.");

            return Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> InsertCarriagePrice([FromBody] CarriagePriceCreateDto dto)
        {
            var success = await _carriagePriceService.InsertCarriagePriceAsync(dto);
            if (!success)
                return BadRequest("Bản ghi đã tồn tại hoặc không thể thêm.");

            return Ok("Thêm thành công.");
        }

        [HttpPut("{scheduleId}/{carriageClassId}")]
        public async Task<IActionResult> UpdateCarriagePrice(int scheduleId, int carriageClassId, [FromBody] CarriagePriceUpdateDto dto)
        {
            var success = await _carriagePriceService.UpdateCarriagePriceAsync(scheduleId, carriageClassId, dto);
            if (!success)
                return NotFound("Không tìm thấy bản ghi để cập nhật.");

            return Ok("Cập nhật thành công.");
        }

    }
}
