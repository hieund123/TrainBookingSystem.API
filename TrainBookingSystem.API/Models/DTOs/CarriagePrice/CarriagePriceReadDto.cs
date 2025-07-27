namespace TrainBookingSystem.API.Models.DTOs.CarriagePrice
{
    public class CarriagePriceReadDto
    {
        public int ScheduleId { get; set; }
        public int CarriageClassId { get; set; }
        public decimal Price { get; set; }
        public string ScheduleName { get; set; } = string.Empty;
        public string CarriageClassName { get; set; } = string.Empty;

    }
}
