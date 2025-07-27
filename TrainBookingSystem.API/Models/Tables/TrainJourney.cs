namespace TrainBookingSystem.API.Models.Tables
{
    public class TrainJourney
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = null!;

        public DateTime DepartureDateTime { get; set; }

        public ICollection<JourneyStation> JourneyStations { get; set; } = new List<JourneyStation>();
        public ICollection<JourneyCarriage> JourneyCarriages { get; set; } = new List<JourneyCarriage>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
