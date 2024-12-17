using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Interfaces;

namespace car_management_backend.Service.Implementations
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;
        private readonly ICarGarageRepository _carGarageRepository;

        public GarageService(IGarageRepository garageRepository, ICarGarageRepository carGarageRepository)
        {
            _garageRepository = garageRepository;
            _carGarageRepository = carGarageRepository;
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
            var carGarages = await _carGarageRepository.GetCarGaragesForGarage(garage.GarageId);

            if (carGarages.Any())
            {
                _carGarageRepository.DeleteCarGarages(carGarages);
                await _carGarageRepository.SaveChangesAsync();
            }

            _garageRepository.DeleteGarage(garage);
            await _garageRepository.SaveChangesAsync();
        }


    }
}
