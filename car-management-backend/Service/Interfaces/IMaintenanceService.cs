using car_management_backend.DTOs.MaintenanceDTOs;
using car_management_backend.Models;
using car_management_backend.Queries;
using Microsoft.AspNetCore.Mvc;

namespace car_management_backend.Service.Interfaces
{
    public interface IMaintenanceService
    {
        IQueryable<Maintenance> GetMaintenances([FromQuery] MaintenanceQueries maintenanceQueries);
        Task<Maintenance?> GetMaintenanceByIdAsync(int maintenanceId);
        Task CreateMaintenanceAsync(Maintenance maintenance);
        Task UpdateMaintenanceAsync(Maintenance maintenance, MaintenanceInPutDTO maintenanceInPutDTO);
        Task DeleteMaintenanceAsync(Maintenance maintenance);
        Task<List<MonthlyRequestsReportDTO>> GetMonthlyGarageReports([FromQuery] MonthlyRequestsReportQueries monthlyRequestsReportQueries);

    }
}
