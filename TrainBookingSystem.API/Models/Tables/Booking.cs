using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; } = null!;

        public int StartingTrainStationId { get; set; }
        public TrainStation? StartingTrainStation { get; set; }

        public int EndingTrainStationId { get; set; }
        public TrainStation? EndingTrainStation { get; set; }

        public int TrainJourneyId { get; set; }
        public TrainJourney? TrainJourney { get; set; }

        public int CarriageClassId { get; set; }
        public CarriageClass? CarriageClass { get; set; }

        public DateTime BookingDate { get; set; }

        public string TicketNo { get; set; } = string.Empty;

        public string SeatNo { get; set; } = string.Empty;

        public decimal AmountPaid { get; set; }

    }
}
