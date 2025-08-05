namespace TrainBookingSystem.API.Models.DTOs.Booking
{
    public class BookingDetailDTO
    {
        public int BookingId { get; set; }
        public string TicketNo { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string SeatNo { get; set; } = string.Empty;
        public decimal AmountPaid { get; set; }

        public string TrainJourneyName { get; set; } = string.Empty;
        public DateTime DepartureDateTime { get; set; }

        public string StartingStationName { get; set; } = string.Empty;
        public string EndingStationName { get; set; } = string.Empty;

        public string CarriageClassName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

    }
}
