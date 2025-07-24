using TrainBookingSystem.API.Models.DTOs.TrainStation;

namespace TrainBookingSystem.API.Services.TrainStation
{
    public interface ITrainStationService
    {
        Task<List<Models.Tables.TrainStation>> GetAllStationsAsync();
        Task<TrainStationReadDTO?> GetStationByIdAsync(int id);
        Task<TrainStationReadDTO> InsertStationAsync(TrainStationCreateDTO dto);
        Task<bool> UpdateStationAsync(int id, TrainStationUpdateDTO dto);
        Task<bool> DeleteStationAsync(int stationId);


    }
}
