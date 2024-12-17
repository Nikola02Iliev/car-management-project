using car_management_backend.DTOs.CarDTOs;
using car_management_backend.Models;

namespace car_management_backend.Service.Interfaces
{
    public interface ICarService
    {
        IQueryable<Car> GetCars();
        Task<Car?> GetCarByIdAsync(int carId);
        Task CreateCarAsync(Car car, List<int> garageIds);
        Task UpdateCarAsync(Car car, CarInPutDTO carInPutDTO, List<int> garageIds);
        Task DeleteCarAsync(Car car);
    }
}
