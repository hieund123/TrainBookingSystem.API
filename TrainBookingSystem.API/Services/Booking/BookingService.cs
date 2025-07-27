using Microsoft.EntityFrameworkCore;
using TrainBookingSystem.API.Data;
using TrainBookingSystem.API.Models.DTOs.Booking;

namespace TrainBookingSystem.API.Services.Booking
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDBContext _context;

        public BookingService(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateBookingAsync(BookingCreateDTO dto)
        {
            // Kiểm tra trùng ghế
            bool seatTaken = await _context.Bookings.AnyAsync(b =>
                b.TrainJourneyId == dto.TrainJourneyId &&
                b.CarriageClassId == dto.CarriageClassId &&
                b.SeatNo == dto.SeatNo);

            if (seatTaken) return false;

            var booking = new Models.Tables.Booking
            {
                UserId = dto.UserId,
                TrainJourneyId = dto.TrainJourneyId,
                StartingTrainStationId = dto.StartingTrainStationId,
                EndingTrainStationId = dto.EndingTrainStationId,
                CarriageClassId = dto.CarriageClassId,
                SeatNo = dto.SeatNo,
                BookingStatusId = 1,
                BookingDate = DateTime.UtcNow,
                TicketNo = Guid.NewGuid().ToString().Substring(0, 10),
                AmountPaid = await GetPriceAsync(dto.TrainJourneyId, dto.CarriageClassId)
            };

            _context.Bookings.Add(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<string>> GetAvailableSeatsAsync(int trainJourneyId, int carriageClassId)
        {
            // Lấy danh sách tất cả ghế của loại toa này (ví dụ giả sử mỗi toa có 40 ghế từ A1 đến A40)
            var allSeats = Enumerable.Range(1, 40).Select(i => $"A{i}").ToList();

            // Lấy danh sách ghế đã đặt
            var bookedSeats = await _context.Bookings
                .Where(b => b.TrainJourneyId == trainJourneyId && b.CarriageClassId == carriageClassId)
                .Select(b => b.SeatNo)
                .ToListAsync();

            return allSeats.Except(bookedSeats).ToList();
        }

        private async Task<decimal> GetPriceAsync(int trainJourneyId, int carriageClassId)
        {
            var journey = await _context.TrainJourneys.Include(j => j.Schedule)
                .FirstOrDefaultAsync(j => j.Id == trainJourneyId);

            var price = await _context.CarriagePrices.FirstOrDefaultAsync(p =>
                p.ScheduleId == journey!.ScheduleId && p.CarriageClassId == carriageClassId);

            return price?.Price ?? 0;
        }

    }
}
