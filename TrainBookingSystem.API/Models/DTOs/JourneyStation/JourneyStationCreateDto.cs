namespace TrainBookingSystem.API.Models.DTOs.JourneyStation
{
    public class JourneyStationCreateDto
    {
        public int TrainStationId { get; set; }
        public int TrainJourneyId { get; set; }
        public int StopOrder { get; set; }
        public TimeSpan DepartureTime { get; set; }
    }
}
