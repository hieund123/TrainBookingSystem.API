namespace TrainBookingSystem.API.Models.DTOs.JourneyStation
{
    public class JourneyStationReadDto
    {
        public int TrainStationId { get; set; }
        public string TrainStationName { get; set; }

        public int StopOrder { get; set; }
        public TimeSpan DepartureTime { get; set; }
    }
}
