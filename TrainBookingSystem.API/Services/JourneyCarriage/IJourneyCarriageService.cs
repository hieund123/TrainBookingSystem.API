using TrainBookingSystem.API.Models.DTOs.JourneyCarriage;

namespace TrainBookingSystem.API.Services.JourneyCarriage
{
    public interface IJourneyCarriageService
    {
        Task<List<JourneyHasCarriageDTO>> GetJourneysHasCarriageAsync(int carriageClassId);
        Task<bool> InsertJourneyCarriageAsync(JourneyCarriageCreateDTO dto);
        Task<List<JourneyCarriageDto>> GetCarriagesInJourneyAsync(int journeyId);

    }
}
