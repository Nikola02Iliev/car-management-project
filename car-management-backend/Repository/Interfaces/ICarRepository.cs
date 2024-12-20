using car_management_backend.DTOs.CarDTOs;
using car_management_backend.Models;

namespace car_management_backend.Repository.Interfaces
{
    public interface ICarRepository
    {
        IQueryable<Car> GetCars();
        Task<Car?> GetCarByIdAsync(int? carId);
        Task CreateCarAsync(Car car);
        void UpdateCar(Car car, CarInPutDTO carInPutDTO);
        void DeleteCar(Car car);
        Task SaveChangesAsync();
    }
}
