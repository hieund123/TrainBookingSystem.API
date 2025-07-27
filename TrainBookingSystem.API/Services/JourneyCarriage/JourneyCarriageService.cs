using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Models.DTOs.JourneyCarriage;

namespace TrainBookingSystem.API.Services.JourneyCarriage
{
    public class JourneyCarriageService : IJourneyCarriageService
    {
        private readonly ApplicationDBContext _context;

        public JourneyCarriageService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<JourneyHasCarriageDTO>> GetJourneysHasCarriageAsync(int carriageClassId)
        {
            return await _context.JourneyCarriages
                .Where(jc => jc.CarriageClassId == carriageClassId)
                .Include(jc => jc.TrainJourney)
                .Select(jc => new JourneyHasCarriageDTO
                {
                    JourneyId = jc.TrainJourney.Id,
                    JourneyName = jc.TrainJourney.Name
                })
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> InsertJourneyCarriageAsync(JourneyCarriageCreateDTO dto)
        {
            // 1. Tăng Position của các toa >= dto.Position
            var affected = await _context.JourneyCarriages
                .Where(jc => jc.TrainJourneyId == dto.TrainJourneyId && jc.Position >= dto.Position)
                .ToListAsync();

            foreach (var item in affected)
            {
                item.Position += 1;
            }

            // 2. Thêm toa mới
            var newJourneyCarriage = new Models.Tables.JourneyCarriage
            {
                TrainJourneyId = dto.TrainJourneyId,
                CarriageClassId = dto.CarriageClassId,
                Position = dto.Position
            };

            _context.JourneyCarriages.Add(newJourneyCarriage);

            // 3. Lưu thay đổi
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<JourneyCarriageDto>> GetCarriagesInJourneyAsync(int journeyId)
        {
            var carriages = await _context.JourneyCarriages
                .Where(jc => jc.TrainJourneyId == journeyId)
                .Include(jc => jc.CarriageClass)
                .OrderBy(jc => jc.Position)
                .ToListAsync();

            return carriages.Select(jc => new JourneyCarriageDto
            {
                Id = jc.Id,
                Position = jc.Position,
                CarriageClassName = jc.CarriageClass.CarriageName,
                TrainJourneyId = jc.TrainJourneyId
            }).ToList();
        }

    }
}
