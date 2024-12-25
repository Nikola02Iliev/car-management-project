using car_management_backend.DTOs.MaintenanceDTOs;
using car_management_backend.Models;
using car_management_backend.Queries;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public IQueryable<Maintenance> GetMaintenances([FromQuery] MaintenanceQueries maintenanceQueries)
        {
            var maintenances = _maintenanceRepository.GetMaintenances();

            if (!string.IsNullOrWhiteSpace(maintenanceQueries.CarName)) 
            {
                maintenances = maintenances.Where(m => m.CarName == maintenanceQueries.CarName);
            }

            if (!string.IsNullOrWhiteSpace(maintenanceQueries.GarageName))
            {
                maintenances = maintenances.Where(m => m.GarageName == maintenanceQueries.GarageName);
            }

            if (!string.IsNullOrWhiteSpace(maintenanceQueries.StartDate))
            {
                DateOnly dateOnly = DateOnly.Parse(maintenanceQueries.StartDate);

                maintenances = maintenances.Where(m => m.ScheduledDate >= dateOnly);
            }

            if (!string.IsNullOrWhiteSpace(maintenanceQueries.EndDate))
            {
                DateOnly dateOnly = DateOnly.Parse(maintenanceQueries.EndDate);

                maintenances = maintenances.Where(m => m.ScheduledDate <= dateOnly);
            }

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

            await _garageRepository.SaveChangesAsync();
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
            var garage = await _garageRepository.GetGarageByIdAsync(maintenance.GarageId);

            _maintenanceRepository.DeleteMaintenance(maintenance);
           
            await _maintenanceRepository.SaveChangesAsync();
        }
    }
}
