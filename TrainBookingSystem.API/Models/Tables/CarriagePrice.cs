namespace TrainBookingSystem.API.Models.Tables
{
    public class CarriagePrice
    {
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = null!;

        public int CarriageClassId { get; set; }
        public CarriageClass CarriageClass { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
