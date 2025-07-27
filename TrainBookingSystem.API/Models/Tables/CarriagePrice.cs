namespace TrainBookingSystem.API.Models.Tables
{
    public class CarriagePrice
    {
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = new Schedule();

        public int CarriageClassId { get; set; }
        public CarriageClass CarriageClass { get; set; } = new CarriageClass();

        public decimal Price { get; set; }

    }
}
