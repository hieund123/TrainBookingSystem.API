namespace TrainBookingSystem.API.Models.DTOs.Booking
{
    public class AvailableSeatsResponseDTO
    {
        public int TrainJourneyId { get; set; }
        public int CarriageClassId { get; set; }
        public List<string> AvailableSeats { get; set; } = new();

    }
}
