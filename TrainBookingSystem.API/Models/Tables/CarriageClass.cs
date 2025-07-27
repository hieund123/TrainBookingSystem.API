using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class CarriageClass
    {
        public int Id { get; set; }

        [Required]
        public string CarriageName { get; set; } = string.Empty;

        public int SeatingCapacity { get; set; }

        public ICollection<JourneyCarriage> JourneyCarriages { get; set; } = new List<JourneyCarriage>();
        public ICollection<CarriagePrice> CarriagePrices { get; set; } = new List<CarriagePrice>();

    }
}
