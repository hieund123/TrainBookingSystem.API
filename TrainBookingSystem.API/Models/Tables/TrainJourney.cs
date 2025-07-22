namespace TrainBookingSystem.API.Models.Tables
{
    public class TrainJourney
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public ICollection<JourneyStation> JourneyStations { get; set; }
        public ICollection<JourneyCarriage> JourneyCarriages { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
