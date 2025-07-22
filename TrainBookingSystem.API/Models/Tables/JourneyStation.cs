namespace TrainBookingSystem.API.Models.Tables
{
    public class JourneyStation
    {
        public int TrainStationId { get; set; }
        public TrainStation TrainStation { get; set; }

        public int TrainJourneyId { get; set; }
        public TrainJourney TrainJourney { get; set; }

        public int StopOrder { get; set; }

        public TimeSpan DepartureTime { get; set; }

    }
}
