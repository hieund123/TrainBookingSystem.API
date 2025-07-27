namespace TrainBookingSystem.API.Models.DTOs.CarriagePrice
{
    public class CarriagePriceToReadDto
    {
        public int ScheduleId { get; set; }
        public string ScheduleName { get; set; } = string.Empty;

        public int CarriageClassId { get; set; }
        public string CarriageClassName { get; set; } = string.Empty;

        public decimal Price { get; set; }

    }
}
