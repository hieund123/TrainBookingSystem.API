using System.ComponentModel.DataAnnotations;
using TrainBookingSystem.API.Models.Tables;

namespace TrainBookingSystem.API.Models
{
    public class BookingStatus
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Booking> Bookings { get; set; }

    }
}
