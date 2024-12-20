using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;

namespace car_management_backend.Repository.Interfaces
{
    public interface IGarageRepository
    {
        IQueryable<Garage> GetGarages();
        Task<Garage?> GetGarageByIdAsync(int? garageId);
        Task CreateGarageAsync(Garage garage);
        void UpdateGarage(Garage garage, GarageInPutDTO garageInPutDTO);
        void DeleteGarage(Garage garage);
        Task SaveChangesAsync();
        


    }
}
