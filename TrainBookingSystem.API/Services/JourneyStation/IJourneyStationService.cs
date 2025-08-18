using TrainBookingSystem.API.Models.DTOs.JourneyStation;

namespace TrainBookingSystem.API.Services.JourneyStation
{
    public interface IJourneyStationService
    {
        Task<List<JourneyStationReadDto>> GetStationsByJourneyIdAsync(int journeyId);
        Task AddJourneyStationAsync(JourneyStationCreateDto dto);
        Task<List<Models.Tables.TrainJourney>> GetJourneysHasStationAsync(int stationId);
        Task DeleteJourneyStationAsync(int journeyId, int stationId);


    }
}
