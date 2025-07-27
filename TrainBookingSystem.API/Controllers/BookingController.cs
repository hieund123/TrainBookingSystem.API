using Microsoft.AspNetCore.Mvc;
using TrainBookingSystem.API.Services.Booking;
using TrainBookingSystem.API.Models.DTOs.Booking;

namespace TrainBookingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDTO dto)
        {
            var success = await _bookingService.CreateBookingAsync(dto);
            if (!success)
                return BadRequest(new { message = "Ghế đã được đặt hoặc có lỗi xảy ra." });

            return Ok(new { message = "Đặt vé thành công." });
        }

        [HttpGet("AvailableSeats")]
        public async Task<IActionResult> GetAvailableSeats(int journeyId, int carriageClassId)
        {
            var seats = await _bookingService.GetAvailableSeatsAsync(journeyId, carriageClassId);
            return Ok(new AvailableSeatsResponseDTO
            {
                TrainJourneyId = journeyId,
                CarriageClassId = carriageClassId,
                AvailableSeats = seats
            });
        }
    }
}
