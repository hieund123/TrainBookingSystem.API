namespace TrainBookingSystem.API.Models.Tables
{
    public class JourneyCarriage
    {
        public int Id { get; set; }

        public int TrainJourneyId { get; set; }
        public TrainJourney TrainJourney { get; set; }

        public int CarriageClassId { get; set; }
        public CarriageClass CarriageClass { get; set; }

        public int Position { get; set; }

    }
}
