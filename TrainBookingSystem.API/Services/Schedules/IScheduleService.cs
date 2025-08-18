using TrainBookingSystem.API.Models.Tables;

namespace TrainBookingSystem.API.Services.Schedules
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedulesAsync();
        Task<Schedule?> GetScheduleByIdAsync(int id);
    }
}
