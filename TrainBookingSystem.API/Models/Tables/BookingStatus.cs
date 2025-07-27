using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.Tables
{
    public class BookingStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
