using car_management_backend.DTOs.MaintenanceDTOs;
using car_management_backend.Models;

namespace car_management_backend.Service.Interfaces
{
    public interface IMaintenanceService
    {
        IQueryable<Maintenance> GetMaintenances();
        Task<Maintenance?> GetMaintenanceByIdAsync(int maintenanceId);
        Task CreateMaintenanceAsync(Maintenance maintenance);
        Task UpdateMaintenanceAsync(Maintenance maintenance, MaintenanceInPutDTO maintenanceInPutDTO);
        Task DeleteMaintenanceAsync(Maintenance maintenance);
        Task<List<DateOnly>> GetAllScheduledDatesInGarage(int garageId);
        
    }
}
