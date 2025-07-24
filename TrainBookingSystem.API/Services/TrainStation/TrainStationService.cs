using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Models.DTOs.TrainStation;

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

        public async Task<TrainStationReadDTO?> GetStationByIdAsync(int id)
        {
            var station = await _context.TrainStations.FindAsync(id);
            if (station == null) return null;

            return new TrainStationReadDTO
            {
                Id = station.Id,
                StationName = station.StationName
            };
        }

        public async Task<TrainStationReadDTO> InsertStationAsync(TrainStationCreateDTO dto)
        {
            var station = new Models.Tables.TrainStation
            {
                StationName = dto.StationName
            };

            _context.TrainStations.Add(station);
            await _context.SaveChangesAsync();

            return new TrainStationReadDTO
            {
                Id = station.Id,
                StationName = station.StationName
            };
        }

        public async Task<bool> UpdateStationAsync(int id, TrainStationUpdateDTO dto)
        {
            var station = await _context.TrainStations.FindAsync(id);
            if (station == null) return false;

            station.StationName = dto.StationName;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStationAsync(int stationId)
        {
            var station = await _context.TrainStations.FindAsync(stationId);
            if (station == null) return false;

            bool isUsed = await _context.JourneyStations.AnyAsync(js => js.TrainStationId == stationId);
            if (isUsed)
                throw new InvalidOperationException("Ga đang được sử dụng trong hành trình.");

            _context.TrainStations.Remove(station);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
