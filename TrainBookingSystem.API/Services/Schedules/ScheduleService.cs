using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Models.Tables;

namespace TrainBookingSystem.API.Services.Schedules
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDBContext _context;

        public ScheduleService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Schedules.ToListAsync();
        }

        public async Task<Schedule?> GetScheduleByIdAsync(int id)
        {
            return await _context.Schedules
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

    }
}
