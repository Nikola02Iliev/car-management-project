using car_management_backend.DTOs.CarDTOs;
using car_management_backend.Models;
using car_management_backend.Queries;
using Microsoft.AspNetCore.Mvc;

namespace car_management_backend.Service.Interfaces
{
    public interface ICarService
    {
        IQueryable<Car> GetCars([FromQuery] CarQueries carQueries);
        Task<Car?> GetCarByIdAsync(int? carId);
        Task CreateCarAsync(Car car, List<int> garageIds);
        Task UpdateCarAsync(Car car, CarInPutDTO carInPutDTO, List<int> garageIds);
        Task DeleteCarAsync(Car car);
        Task<List<Car>> GetCarsInGarageAsync(int garageId);
        Task<List<int?>> GetCarIdsInMaintenanceInGarage(int garageId);
    }
}
