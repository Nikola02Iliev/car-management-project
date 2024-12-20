using car_management_backend.DTOs.MaintenanceDTOs;
using car_management_backend.Models;

namespace car_management_backend.Repository.Interfaces
{
    public interface IMaintenanceRepository
    {
        IQueryable<Maintenance> GetMaintenances();
        Task<Maintenance?> GetMaintenanceByIdAsync(int maintenanceId);
        Task CreateMaintenanceAsync(Maintenance maintenance);
        void UpdateMaintenance(Maintenance maintenance, MaintenanceInPutDTO maintenanceInPutDTO);
        void DeleteMaintenance(Maintenance maintenance);
        void DeleteMaintenances(List<Maintenance> maintenances);
        Task<List<Maintenance>> GetMaintenancesForGarage(int garageId);
        Task<List<Maintenance>> GetMaintenancesForCar(int carId);

        Task SaveChangesAsync();

    }
}
