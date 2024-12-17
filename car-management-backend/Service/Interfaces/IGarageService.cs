using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;

namespace car_management_backend.Service.Interfaces
{
    public interface IGarageService
    {
        IQueryable<Garage> GetGarages();
        Task<Garage?> GetGarageByIdAsync(int garageId);
        Task CreateGarageAsync(Garage garage);
        Task UpdateGarageAsync(Garage garage, GarageInPutDTO garageInPutDTO);
        Task DeleteGarageAsync(Garage garage);
    }
}
