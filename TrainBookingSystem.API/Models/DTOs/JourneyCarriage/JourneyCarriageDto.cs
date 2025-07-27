namespace TrainBookingSystem.API.Models.DTOs.JourneyCarriage
{
    public class JourneyCarriageDto
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string CarriageClassName { get; set; } = string.Empty;
        public int TrainJourneyId { get; set; }

    }
}
