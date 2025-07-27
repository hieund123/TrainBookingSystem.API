namespace TrainBookingSystem.API.Models.DTOs.JourneyCarriage
{
    public class JourneyCarriageCreateDTO
    {
        public int TrainJourneyId { get; set; }
        public int CarriageClassId { get; set; }
        public int Position { get; set; }

    }
}
