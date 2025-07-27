namespace TrainBookingSystem.API.Models.Tables
{
    public class JourneyStation
    {
        public int TrainStationId { get; set; }
        public TrainStation TrainStation { get; set; } = new TrainStation();

        public int TrainJourneyId { get; set; }
        public TrainJourney TrainJourney { get; set; } = new TrainJourney();

        public int StopOrder { get; set; }

        public TimeSpan DepartureTime { get; set; }

    }
}
