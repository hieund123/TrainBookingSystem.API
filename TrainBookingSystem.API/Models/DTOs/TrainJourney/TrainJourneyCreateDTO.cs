using System.ComponentModel.DataAnnotations;

namespace TrainBookingSystem.API.Models.DTOs.TrainJourney
{
    public class TrainJourneyCreateDTO
    {
        public string Name { get; set; }
        public int ScheduleId { get; set; }
    }
}
