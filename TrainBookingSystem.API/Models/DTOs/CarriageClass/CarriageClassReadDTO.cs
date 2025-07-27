namespace TrainBookingSystem.API.Models.DTOs.CarriageClass
{
    public class CarriageClassReadDTO
    {
        public int Id { get; set; }
        public string CarriageName { get; set; } = string.Empty;
        public int SeatingCapacity { get; set; }

    }
}
