namespace TrainBookingSystem.API.Models.DTOs.TrainJourney
{
    public class TrainJourneyReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }
        public int ScheduleId { get; set; }
        public string? ScheduleName { get; set; }
        public DateTime DepartureDateTime { get; set; }
    }
}
