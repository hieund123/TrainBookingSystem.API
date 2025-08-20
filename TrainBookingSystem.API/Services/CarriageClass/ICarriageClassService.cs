using TrainBookingSystem.API.Models.DTOs.CarriageClass;

namespace TrainBookingSystem.API.Services.CarriageClass
{
    public interface ICarriageClassService
    {
        Task<List<CarriageClassReadDTO>> GetAllCarriageClassesAsync();
        Task<CarriageClassReadDTO> GetCarriageClassByIdAsync(int id);
        Task<bool> InsertCarriageClassAsync(CarriageClassCreateDTO dto);
        Task<bool> DeleteCarriageClassAsync(int id, bool force = false);
        Task<bool> UpdateCarriageClassAsync(int id, CarriageClassUpdateDTO dto);

    }
}
