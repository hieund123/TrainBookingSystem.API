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
                ScheduleId = dto.ScheduleId
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
                ScheduleName = schedule?.Name
            };
        }
    }
}
