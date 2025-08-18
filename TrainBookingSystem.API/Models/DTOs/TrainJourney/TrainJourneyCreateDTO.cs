using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.DTOs.TrainJourney
{
    public class TrainJourneyCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public int ScheduleId { get; set; }
        public DateTime DepartureDateTime { get; set; }
    }
}
