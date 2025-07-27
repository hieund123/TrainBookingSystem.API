namespace TrainBookingSystem.API.Models.DTOs.TrainJourney
{
    public class TrainJourneyDto
    {
        public int Id { get; set; }
        public string TrainName { get; set; } = string.Empty;
        public DateTime DepartureDateTime { get; set; }
        public string StartStationName { get; set; } = string.Empty;
        public string EndStationName { get; set; } = string.Empty;
    }
}
