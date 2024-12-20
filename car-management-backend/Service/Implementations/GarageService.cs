﻿using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend.Service.Implementations
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;
        private readonly ICarGarageRepository _carGarageRepository;
        private readonly IMaintenanceRepository _maintenanceRepository;

        public GarageService(IGarageRepository garageRepository, ICarGarageRepository carGarageRepository, IMaintenanceRepository maintenanceRepository)
        {
            _garageRepository = garageRepository;
            _carGarageRepository = carGarageRepository;
            _maintenanceRepository = maintenanceRepository;
        }

        public IQueryable<Garage> GetGarages()
        {
            var garages = _garageRepository.GetGarages();

            return garages;
        }

        public async Task<Garage?> GetGarageByIdAsync(int? garageId)
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
            var maintenances = await _maintenanceRepository.GetMaintenancesForGarage(garage.GarageId);

            if (maintenances.Any())
            {
                _maintenanceRepository.DeleteMaintenances(maintenances);
                await _maintenanceRepository.SaveChangesAsync();
            }

            if (carGarages.Any())
            {
                _carGarageRepository.DeleteCarGarages(carGarages);
                await _carGarageRepository.SaveChangesAsync();
            }

            _garageRepository.DeleteGarage(garage);
            await _garageRepository.SaveChangesAsync();
        }

        public async Task<List<int>> GetAllGarageIdsAsync()
        {
            var garagesIds = await _garageRepository.GetGarages().Select(g => g.GarageId).ToListAsync();

            return garagesIds;
        }

        public async Task<List<int?>> GetAllGarageIdsAsyncForCar(int carId)
        {
            var garageIds = await _carGarageRepository.GetGarageIdsForCar(carId);

            return garageIds;
        }
    }
}
