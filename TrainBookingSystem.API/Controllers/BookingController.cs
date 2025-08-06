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

        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetBookingsByUserId([FromQuery] string userId)
        {
            var result = await _bookingService.GetBookingsByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPost("Cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var result = await _bookingService.CancelBookingAsync(bookingId);
            if (!result)
                return BadRequest(new { message = "Không thể hủy vé này." });

            return Ok(new { message = "Hủy vé thành công." });
        }


    }
}
