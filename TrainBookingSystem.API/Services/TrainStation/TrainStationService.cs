using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;

namespace TrainBookingSystem.API.Services.TrainStation
{
    public class TrainStationService : ITrainStationService
    {
        private readonly ApplicationDBContext _context;

        public TrainStationService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Tables.TrainStation>> GetAllStationsAsync()
        {
            return await _context.TrainStations.ToListAsync();
        }
    }
}
