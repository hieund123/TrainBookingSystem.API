namespace TrainBookingSystem.API.Models.DTOs.Booking
{
    public class BookingCreateDTO
    {
        public string UserId { get; set; } = string.Empty;
        public int TrainJourneyId { get; set; }
        public int StartingTrainStationId { get; set; }
        public int EndingTrainStationId { get; set; }
        public int CarriageClassId { get; set; }
        public string SeatNo { get; set; } = string.Empty;

    }
}
