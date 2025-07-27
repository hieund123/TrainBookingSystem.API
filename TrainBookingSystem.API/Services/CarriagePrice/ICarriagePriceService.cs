using TrainBookingSystem.API.Models.DTOs.CarriagePrice;

namespace TrainBookingSystem.API.Services.CarriagePrice
{
    public interface ICarriagePriceService
    {
        Task<IEnumerable<CarriagePriceToReadDto>> GetAllCarriagePricesAsync();
        Task<CarriagePriceReadDto> GetCarriagePriceByIdAsync(int scheduleId, int carriageClassId);
        Task<bool> InsertCarriagePriceAsync(CarriagePriceCreateDto dto);
        Task<bool> UpdateCarriagePriceAsync(int scheduleId, int carriageClassId, CarriagePriceUpdateDto dto);

    }
}
