using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class BookingStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Booking> Bookings { get; set; }

    }
}
