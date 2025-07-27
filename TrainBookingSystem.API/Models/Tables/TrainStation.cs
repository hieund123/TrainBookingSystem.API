using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class TrainStation
    {
        public int Id { get; set; }

        [Required]
        public string StationName { get; set; } = string.Empty;
        public ICollection<JourneyStation> JourneyStations { get; set; } = new List<JourneyStation>();

    }
}
