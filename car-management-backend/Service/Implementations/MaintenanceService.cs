﻿using car_management_backend.DTOs.MaintenanceDTOs;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend.Service.Implementations
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly ICarRepository _carRepository;

        public MaintenanceService(IMaintenanceRepository maintenanceRepository, IGarageRepository garageRepository, ICarRepository carRepository)
        {
            _maintenanceRepository = maintenanceRepository;
            _garageRepository = garageRepository;
            _carRepository = carRepository;
        }

        public IQueryable<Maintenance> GetMaintenances()
        {
            var maintenances = _maintenanceRepository.GetMaintenances();

            return maintenances;
        }

        public async Task<Maintenance?> GetMaintenanceByIdAsync(int maintenanceId)
        {
            var maintenance = await _maintenanceRepository.GetMaintenanceByIdAsync(maintenanceId);

            return maintenance;
        }

        public async Task CreateMaintenanceAsync(Maintenance maintenance)
        {
            var garage = await _garageRepository.GetGarageByIdAsync(maintenance.GarageId);
            var car = await _carRepository.GetCarByIdAsync(maintenance.CarId);

            maintenance.GarageName = garage.Name;
            maintenance.CarName = car.Make;

            await _maintenanceRepository.CreateMaintenanceAsync(maintenance);

            await _maintenanceRepository.SaveChangesAsync();
        }

        public async Task UpdateMaintenanceAsync(Maintenance maintenance, MaintenanceInPutDTO maintenanceInPutDTO)
        {
            var garage = await _garageRepository.GetGarageByIdAsync(maintenance.GarageId);
            var car = await _carRepository.GetCarByIdAsync(maintenance.CarId);

            maintenance.GarageName = garage.Name;
            maintenance.CarName = car.Make;

            _maintenanceRepository.UpdateMaintenance(maintenance, maintenanceInPutDTO);
            await _maintenanceRepository.SaveChangesAsync();
        }

        public async Task DeleteMaintenanceAsync(Maintenance maintenance)
        {
            _maintenanceRepository.DeleteMaintenance(maintenance);

            await _maintenanceRepository.SaveChangesAsync();
        }

        public Task<List<DateOnly>> GetAllScheduledDatesInGarage(int garageId)
        {
            var scheduledDates = _maintenanceRepository.GetMaintenances().Select(m => m.ScheduledDate).ToListAsync();

            return scheduledDates;
        }
    }
}
