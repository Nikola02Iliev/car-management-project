using car_management_backend.DTOs.Garage;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Interfaces;

namespace car_management_backend.Service.Implementations
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;

        public GarageService(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }

        public IQueryable<Garage> GetGarages()
        {
            var garages = _garageRepository.GetGarages();

            return garages;
        }

        public async Task<Garage?> GetGarageByIdAsync(int garageId)
        {
            var garage = await _garageRepository.GetGarageByIdAsync(garageId);
            
            return garage;
        }

        public async Task CreateGarageAsync(Garage garage)
        {
            await _garageRepository.CreateGarageAsync(garage);
            await _garageRepository.SaveChangesAsync();
        }

        public async Task UpdateGarageAsync(Garage garage, GarageInPutDTO garageInPutDTO)
        {
            _garageRepository.UpdateGarage(garage, garageInPutDTO);
            await _garageRepository.SaveChangesAsync();
        }

        public async Task DeleteGarageAsync(Garage garage)
        {
            _garageRepository.DeleteGarage(garage);
            await _garageRepository.SaveChangesAsync();
        }
    }
}
