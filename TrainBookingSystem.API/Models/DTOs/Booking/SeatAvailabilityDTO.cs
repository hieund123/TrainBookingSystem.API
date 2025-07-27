namespace TrainBookingSystem.API.Models.DTOs.Booking
{
    public class SeatAvailabilityDTO
    {
        public string SeatNo { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }

    }
}
