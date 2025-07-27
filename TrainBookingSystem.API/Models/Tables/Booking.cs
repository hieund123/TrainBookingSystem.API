using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty; // FK tới AspNetUsers.Id

        public int BookingStatusId { get; set; }
        public BookingStatus BookingStatus { get; set; } = new BookingStatus();

        public int StartingTrainStationId { get; set; }
        public TrainStation StartingTrainStation { get; set; } = new TrainStation();

        public int EndingTrainStationId { get; set; }
        public TrainStation EndingTrainStation { get; set; } = new TrainStation();

        public int TrainJourneyId { get; set; }
        public TrainJourney TrainJourney { get; set; } = new TrainJourney();

        public int CarriageClassId { get; set; }
        public CarriageClass CarriageClass { get; set; } = new CarriageClass();

        public DateTime BookingDate { get; set; }

        public string TicketNo { get; set; }  = string.Empty ;

        public string SeatNo { get; set; } = string.Empty ;

        public decimal AmountPaid { get; set; }

    }
}
