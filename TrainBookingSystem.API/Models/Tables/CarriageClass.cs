using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class CarriageClass
    {
        public int Id { get; set; }

        [Required]
        public string CarriageName { get; set; }

        public int SeatingCapacity { get; set; }

        public ICollection<JourneyCarriage> JourneyCarriages { get; set; }
        public ICollection<CarriagePrice> CarriagePrices { get; set; }

    }
}
