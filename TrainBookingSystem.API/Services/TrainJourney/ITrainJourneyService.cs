using TrainBookingSystem.API.Models.DTOs.TrainJourney;

namespace TrainBookingSystem.API.Services.TrainJourney
{
    public interface ITrainJourneyService
    {
        Task<List<TrainJourneyReadDTO>> GetAllJourneysAsync();
        Task<TrainJourneyReadDTO?> GetJourneyByIdAsync(int id);
        Task<bool> IsJourneyExistAsync(string journeyName);
        Task<TrainJourneyReadDTO> InsertJourneyAsync(TrainJourneyCreateDTO dto);
        Task<List<TrainJourneyDto>> SearchJourneys(
            DateTime? departureDate,
            int? startStationId,
            int? endStationId
        );
        Task<List<TrainJourneyReadDTO>> GetJourneysByScheduleIdAsync(int scheduleId);
        Task<bool> DeleteJourneyAsync(int id);

    }
}
