namespace TrainBookingSystem.API.Models.DTOs
{
    public class JourneyStationDto
    {
        public int TrainStationId { get; set; }
        public int StopOrder { get; set; }
        public TimeSpan DepartureTime { get; set; }

    }
}
