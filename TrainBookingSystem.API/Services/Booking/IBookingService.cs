using TrainBookingSystem.API.Models.DTOs.Booking;

namespace TrainBookingSystem.API.Services.Booking
{
    public interface IBookingService
    {
        Task<bool> CreateBookingAsync(BookingCreateDTO dto);
        Task<List<string>> GetAvailableSeatsAsync(int trainJourneyId, int carriageClassId);

    }
}
