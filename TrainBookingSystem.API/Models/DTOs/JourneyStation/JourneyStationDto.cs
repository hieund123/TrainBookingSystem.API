namespace TrainBookingSystem.API.Models.DTOs.JourneyStation
{
    public class JourneyStationDto
    {
        public int StopOrder { get; set; }
        public string StationName { get; set; } = string.Empty;
        public TimeSpan DepartureTime { get; set; }

    }
}
