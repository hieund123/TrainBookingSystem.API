using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Models.DTOs.CarriageClass;

namespace TrainBookingSystem.API.Services.CarriageClass
{
    public class CarriageClassService : ICarriageClassService
    {
        private readonly ApplicationDBContext _context;

        public CarriageClassService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<CarriageClassReadDTO>> GetAllCarriageClassesAsync()
        {
            var classes = await _context.CarriageClasses.ToListAsync();
            return classes.Select(c => new CarriageClassReadDTO
            {
                Id = c.Id,
                CarriageName = c.CarriageName,
                SeatingCapacity = c.SeatingCapacity
            }).ToList();
        }

        public async Task<CarriageClassReadDTO> GetCarriageClassByIdAsync(int id)
        {
            var entity = await _context.CarriageClasses.FindAsync(id);
            if (entity == null) return null;

            return new CarriageClassReadDTO
            {
                Id = entity.Id,
                CarriageName = entity.CarriageName,
                SeatingCapacity = entity.SeatingCapacity
            };
        }

        public async Task<bool> InsertCarriageClassAsync(CarriageClassCreateDTO dto)
        {
            var entity = new Models.Tables.CarriageClass
            {
                CarriageName = dto.CarriageName,
                SeatingCapacity = dto.SeatingCapacity
            };

            _context.CarriageClasses.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCarriageClassAsync(int id, bool force = false)
        {
            var carriageClass = await _context.CarriageClasses.FindAsync(id);
            if (carriageClass == null) return false;

            if (force)
            {
                var related = _context.JourneyCarriages.Where(jc => jc.CarriageClassId == id);
                _context.JourneyCarriages.RemoveRange(related);
                await _context.SaveChangesAsync(); // Xóa trước liên kết
            }

            _context.CarriageClasses.Remove(carriageClass);
            await _context.SaveChangesAsync(); // xóa chính nó

            return true;
        }


    }
}
