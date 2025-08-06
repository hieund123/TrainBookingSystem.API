namespace TrainBookingSystem.API.Models.DTOs.Booking
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public string TicketNo { get; set; } = string.Empty;
        public string SeatNo { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public decimal AmountPaid { get; set; }

        public string Status { get; set; } = string.Empty;

        public string StartingStationName { get; set; } = string.Empty;
        public string EndingStationName { get; set; } = string.Empty;
        public string TrainName { get; set; } = string.Empty;

    }
}
