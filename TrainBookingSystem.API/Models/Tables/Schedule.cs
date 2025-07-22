using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<TrainJourney> TrainJourneys { get; set; }
        public ICollection<CarriagePrice> CarriagePrices { get; set; }

    }
}
