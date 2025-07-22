namespace TrainBookingSystem.API.Services.TrainStation
{
    public interface ITrainStationService
    {
        Task<List<Models.Tables.TrainStation>> GetAllStationsAsync();

    }
}
