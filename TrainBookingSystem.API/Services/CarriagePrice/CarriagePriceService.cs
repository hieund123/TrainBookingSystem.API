using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Models.DTOs.CarriagePrice;

namespace TrainBookingSystem.API.Services.CarriagePrice
{
    public class CarriagePriceService : ICarriagePriceService
    {
        private readonly ApplicationDBContext _context;

        public CarriagePriceService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarriagePriceToReadDto>> GetAllCarriagePricesAsync()
        {
            return await _context.CarriagePrices
                .Include(cp => cp.Schedule)
                .Include(cp => cp.CarriageClass)
                .Select(cp => new CarriagePriceToReadDto
                {
                    ScheduleId = cp.ScheduleId,
                    ScheduleName = cp.Schedule.Name,
                    CarriageClassId = cp.CarriageClassId,
                    CarriageClassName = cp.CarriageClass.CarriageName,
                    Price = cp.Price
                })
                .ToListAsync();
        }

        public async Task<CarriagePriceReadDto> GetCarriagePriceByIdAsync(int scheduleId, int carriageClassId)
        {
            var entity = await _context.CarriagePrices
                .Include(cp => cp.Schedule)
                .Include(cp => cp.CarriageClass)
                .FirstOrDefaultAsync(cp => cp.ScheduleId == scheduleId && cp.CarriageClassId == carriageClassId);

            if (entity == null) return null;

            return new CarriagePriceReadDto
            {
                ScheduleId = entity.ScheduleId,
                ScheduleName = entity.Schedule.Name,
                CarriageClassId = entity.CarriageClassId,
                CarriageClassName = entity.CarriageClass.CarriageName,
                Price = entity.Price
            };
        }

        public async Task<bool> InsertCarriagePriceAsync(CarriagePriceCreateDto dto)
        {
            var exists = await _context.CarriagePrices
                .AnyAsync(cp => cp.ScheduleId == dto.ScheduleId && cp.CarriageClassId == dto.CarriageClassId);

            if (exists) return false;

            var entity = new Models.Tables.CarriagePrice
            {
                ScheduleId = dto.ScheduleId,
                CarriageClassId = dto.CarriageClassId,
                Price = dto.Price
            };

            _context.CarriagePrices.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCarriagePriceAsync(int scheduleId, int carriageClassId, CarriagePriceUpdateDto dto)
        {
            var entity = await _context.CarriagePrices
                .FirstOrDefaultAsync(cp => cp.ScheduleId == scheduleId && cp.CarriageClassId == carriageClassId);

            if (entity == null) return false;

            entity.Price = dto.Price;
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
