using car_management_backend.Context;
using car_management_backend.DTOs.MaintenanceDTOs;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend.Repository.Implementations
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly AppDBContext _context;
        private readonly DbSet<Maintenance> _dbSet;

        public MaintenanceRepository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<Maintenance>();
        }

        public IQueryable<Maintenance> GetMaintenances()
        {
            var maintenances = _dbSet.AsQueryable();

            return maintenances;
        }

        public async Task<Maintenance?> GetMaintenanceByIdAsync(int maintenanceId)
        {
            var maintenance = await _dbSet.SingleOrDefaultAsync(m => m.MaintenanceId == maintenanceId);

            return maintenance;
        }

        public async Task CreateMaintenanceAsync(Maintenance maintenance)
        {
            await _dbSet.AddAsync(maintenance);
        }

        public void UpdateMaintenance(Maintenance maintenance, MaintenanceInPutDTO maintenanceInPutDTO)
        {
            maintenance.ScheduledDate = DateOnly.Parse(maintenanceInPutDTO.ScheduledDate);
            maintenance.ServiceType = maintenanceInPutDTO.ServiceType;
            maintenance.CarId = maintenanceInPutDTO.CarId;
            maintenance.GarageId = maintenanceInPutDTO.GarageId;
        }

        public void DeleteMaintenance(Maintenance maintenance)
        {
            _dbSet.Remove(maintenance);
        }

        public void DeleteMaintenances(List<Maintenance> maintenances)
        {
            _dbSet.RemoveRange(maintenances);
        }

        public async Task<List<Maintenance>> GetMaintenancesForGarage(int garageId)
        {
            var maintenances = await _dbSet.Where(m => m.GarageId == garageId).ToListAsync();

            return maintenances;
        }
        public async Task<List<Maintenance>> GetMaintenancesForCar(int carId)
        {
            var maintenances = await _dbSet.Where(m => m.CarId == carId).ToListAsync();

            return maintenances;

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
