using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<TrainJourney> TrainJourneys { get; set; } = new List<TrainJourney>();
        public ICollection<CarriagePrice> CarriagePrices { get; set; } = new List<CarriagePrice>();

    }
}
