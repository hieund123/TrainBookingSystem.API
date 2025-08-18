using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Models.DTOs.TrainJourney;

namespace TrainBookingSystem.API.Services.TrainJourney
{
    public class TrainJourneyService : ITrainJourneyService
    {
        private readonly ApplicationDBContext _context;

        public TrainJourneyService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<TrainJourneyReadDTO>> GetAllJourneysAsync()
        {
            var journeys = await _context.TrainJourneys
                .Include(j => j.Schedule)
                .ToListAsync();

            return journeys.Select(j => new TrainJourneyReadDTO
            {
                Id = j.Id,
                Name = j.Name,
                DateTime = j.DepartureDateTime,
                ScheduleId = j.ScheduleId,
                ScheduleName = j.Schedule?.Name
            }).ToList();
        }

        public async Task<TrainJourneyReadDTO?> GetJourneyByIdAsync(int id)
        {
            var journey = await _context.TrainJourneys
                .Include(j => j.Schedule)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (journey == null) return null;

            return new TrainJourneyReadDTO
            {
                Id = journey.Id,
                Name = journey.Name,
                ScheduleId = journey.ScheduleId,
                ScheduleName = journey.Schedule?.Name
            };
        }


        public async Task<bool> IsJourneyExistAsync(string journeyName)
        {
            return await _context.TrainJourneys.AnyAsync(j => j.Name == journeyName);
        }

        public async Task<TrainJourneyReadDTO> InsertJourneyAsync(TrainJourneyCreateDTO dto)
        {
            if (await IsJourneyExistAsync(dto.Name))
                throw new InvalidOperationException("Hành trình đã tồn tại.");

            var newJourney = new Models.Tables.TrainJourney
            {
                Name = dto.Name,
                ScheduleId = dto.ScheduleId,
                DepartureDateTime = dto.DepartureDateTime
            };

            _context.TrainJourneys.Add(newJourney);
            await _context.SaveChangesAsync();

            // Load lại Schedule để map
            var schedule = await _context.Schedules.FindAsync(newJourney.ScheduleId);

            return new TrainJourneyReadDTO
            {
                Id = newJourney.Id,
                Name = newJourney.Name,
                ScheduleId = newJourney.ScheduleId,
                ScheduleName = schedule?.Name,
                DepartureDateTime = newJourney.DepartureDateTime
            };
        }

        public async Task<List<TrainJourneyDto>> SearchJourneys(DateTime? departureDate, int? startStationId, int? endStationId)
        {
            var query = _context.TrainJourneys
                .Include(j => j.JourneyStations)
                    .ThenInclude(js => js.TrainStation)
                .AsQueryable();

            if (departureDate.HasValue)
            {
                query = query.Where(j => j.DepartureDateTime.Date == departureDate.Value.Date);
            }

            // Lọc hành trình có chứa cả start và end station
            if (startStationId.HasValue && endStationId.HasValue)
            {
                query = query.Where(j =>
                    j.JourneyStations.Any(js => js.TrainStationId == startStationId.Value) &&
                    j.JourneyStations.Any(js => js.TrainStationId == endStationId.Value));
            }

            var journeys = await query.ToListAsync();

            var result = journeys
                .Where(j =>
                {
                    var ordered = j.JourneyStations.OrderBy(js => js.StopOrder).ToList();

                    if (startStationId.HasValue && endStationId.HasValue)
                    {
                        var startIndex = ordered.FindIndex(js => js.TrainStationId == startStationId);
                        var endIndex = ordered.FindIndex(js => js.TrainStationId == endStationId);
                        return startIndex != -1 && endIndex != -1 && startIndex < endIndex;
                    }

                    return true;
                })
                .Select(j =>
                {
                    var orderedStations = j.JourneyStations.OrderBy(js => js.StopOrder).ToList();
                    var start = orderedStations.FirstOrDefault(js => js.TrainStationId == startStationId);
                    var end = orderedStations.LastOrDefault(js => js.TrainStationId == endStationId);

                    return new TrainJourneyDto
                    {
                        Id = j.Id,
                        TrainName = j.Name,
                        DepartureDateTime = j.DepartureDateTime,
                        StartStationName = start?.TrainStation?.StationName,
                        EndStationName = end?.TrainStation?.StationName
                    };
                }).ToList();

            return result;
        }

        public async Task<List<TrainJourneyReadDTO>> GetJourneysByScheduleIdAsync(int scheduleId)
        {
            var journeys = await _context.TrainJourneys
                .Include(j => j.Schedule)
                .Where(j => j.ScheduleId == scheduleId)
                .ToListAsync();

            return journeys.Select(j => new TrainJourneyReadDTO
            {
                Id = j.Id,
                Name = j.Name,
                DateTime = j.DepartureDateTime,
                ScheduleId = j.ScheduleId,
                ScheduleName = j.Schedule?.Name
            }).ToList();
        }

        public async Task<bool> DeleteJourneyAsync(int id)
        {
            var journey = await _context.TrainJourneys
                .Include(j => j.JourneyStations)
                .Include(j => j.JourneyCarriages)
                .Include(j => j.Bookings)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (journey == null)
                return false;

            // Xóa các liên kết trước khi xóa chính journey
            _context.JourneyStations.RemoveRange(journey.JourneyStations);
            _context.JourneyCarriages.RemoveRange(journey.JourneyCarriages);
            _context.Bookings.RemoveRange(journey.Bookings);

            _context.TrainJourneys.Remove(journey);

            return await _context.SaveChangesAsync() > 0;
        }

    }
}
