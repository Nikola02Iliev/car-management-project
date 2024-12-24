using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;
using car_management_backend.Queries;
using Microsoft.AspNetCore.Mvc;

namespace car_management_backend.Service.Interfaces
{
    public interface IGarageService
    {
        IQueryable<Garage> GetGarages([FromQuery] GarageQueries garageQueries);
        Task<Garage?> GetGarageByIdAsync(int? garageId);
        Task CreateGarageAsync(Garage garage);
        Task UpdateGarageAsync(Garage garage, GarageInPutDTO garageInPutDTO);
        Task DeleteGarageAsync(Garage garage);
        Task<List<int>> GetAllGarageIdsAsync();
        Task<List<int?>> GetAllGarageIdsAsyncForCar(int carId);

    }
}
