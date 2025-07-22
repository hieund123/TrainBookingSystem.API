using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Models.DTOs.JourneyStation;

namespace TrainBookingSystem.API.Services.JourneyStation
{
    public class JourneyStationService : IJourneyStationService
    {
        private readonly ApplicationDBContext _context;

        public JourneyStationService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<JourneyStationReadDto>> GetStationsByJourneyIdAsync(int journeyId)
        {
            return await _context.JourneyStations
                .Where(js => js.TrainJourneyId == journeyId)
                .Include(js => js.TrainStation)
                .OrderBy(js => js.StopOrder)
                .Select(js => new JourneyStationReadDto
                {
                    TrainStationId = js.TrainStationId,
                    TrainStationName = js.TrainStation.StationName,
                    StopOrder = js.StopOrder,
                    DepartureTime = js.DepartureTime
                }).ToListAsync();
        }

        public async Task AddJourneyStationAsync(JourneyStationCreateDto dto)
        {
            var newJourneyStation = new Models.Tables.JourneyStation
            {
                TrainStationId = dto.TrainStationId,
                TrainJourneyId = dto.TrainJourneyId,
                StopOrder = dto.StopOrder,
                DepartureTime = dto.DepartureTime
            };

            _context.JourneyStations.Add(newJourneyStation);
            await _context.SaveChangesAsync();
        }
    }
}
