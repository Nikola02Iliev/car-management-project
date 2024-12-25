using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;
using car_management_backend.Queries;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        public IQueryable<Garage> GetGarages([FromQuery] GarageQueries garageQueries)
        {
            var garages = _garageRepository.GetGarages();

            if (!string.IsNullOrWhiteSpace(garageQueries.City))
            {
                garages = garages.Where(g => g.City == garageQueries.City);
            }

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

        public async Task<IQueryable<GarageReportDTO>> GetDailyGarageReports([FromQuery] DailyGarageReportQueries dailyGarageReportQueries)
        {
            var garage = await _garageRepository.GetGarageByIdAsync(dailyGarageReportQueries.GarageId);
            var allScheduledDates = await _maintenanceRepository.GetMaintenances().Where(m => m.GarageId == garage.GarageId).Select(m => m.ScheduledDate).ToListAsync();

            var reports = new List<GarageReportDTO>();
            var seenDates = new Dictionary<string, int>();

            foreach (var scheduledDate in allScheduledDates)
            {
                var formattedDate = scheduledDate.ToString("yyyy-MM-dd");

                if (seenDates.ContainsKey(formattedDate))
                {
                    seenDates[formattedDate]++;
                }
                else
                {
                    seenDates[formattedDate] = 1;
                }
            }

            foreach (var entry in seenDates)
            {
                var availableCapacity = garage.Capacity - entry.Value;

                var report = new GarageReportDTO
                {
                    Date = entry.Key,
                    Requests = entry.Value,
                    AvailableCapacity = availableCapacity > 0 ? availableCapacity : 0
                };

                reports.Add(report);
            }

            var reportsAsQueryable = reports.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dailyGarageReportQueries.StartDate))
            {
                reportsAsQueryable = reportsAsQueryable.Where(r => DateOnly.Parse(r.Date) >= DateOnly.Parse(dailyGarageReportQueries.StartDate));
            }

            if (!string.IsNullOrWhiteSpace(dailyGarageReportQueries.EndDate))
            {
                reportsAsQueryable = reportsAsQueryable.Where(r => DateOnly.Parse(r.Date) <= DateOnly.Parse(dailyGarageReportQueries.EndDate));
            }

            return reportsAsQueryable;

        }
    }
}
