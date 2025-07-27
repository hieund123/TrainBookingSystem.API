namespace TrainBookingSystem.API.Models.Tables
{
    public class JourneyCarriage
    {
        public int Id { get; set; }

        public int TrainJourneyId { get; set; }
        public TrainJourney TrainJourney { get; set; } = new TrainJourney();

        public int CarriageClassId { get; set; }
        public CarriageClass CarriageClass { get; set; } = new CarriageClass();

        public int Position { get; set; }

    }
}
