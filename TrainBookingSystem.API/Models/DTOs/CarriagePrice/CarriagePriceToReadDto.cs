namespace TrainBookingSystem.API.Models.DTOs.CarriagePrice
{
    public class CarriagePriceToReadDto
    {
        public int ScheduleId { get; set; }
        public string ScheduleName { get; set; }

        public int CarriageClassId { get; set; }
        public string CarriageClassName { get; set; }

        public decimal Price { get; set; }

    }
}
