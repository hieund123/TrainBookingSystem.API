using TrainBookingSystem.API.Models.DTOs.JourneyStation;

namespace TrainBookingSystem.API.Services.JourneyStation
{
    public interface IJourneyStationService
    {
        Task<List<JourneyStationReadDto>> GetStationsByJourneyIdAsync(int journeyId);
        Task AddJourneyStationAsync(JourneyStationCreateDto dto);
    }
}
