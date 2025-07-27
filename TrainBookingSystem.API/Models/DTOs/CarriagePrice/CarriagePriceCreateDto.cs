namespace TrainBookingSystem.API.Models.DTOs.CarriagePrice
{
    public class CarriagePriceCreateDto
    {
        public int ScheduleId { get; set; }
        public int CarriageClassId { get; set; }
        public decimal Price { get; set; }

    }
}
