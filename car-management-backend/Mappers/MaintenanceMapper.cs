using car_management_backend.DTOs.MaintenanceDTOs;
using car_management_backend.Models;

namespace car_management_backend.Mappers
{
    public static class MaintenanceMapper
    {
        public static MaintenanceInListDTO ConvertMaintenanceToMaintenanceInListDTO(this Maintenance maintenance)
        {
            return new MaintenanceInListDTO
            {
                Id = maintenance.MaintenanceId,
                ServiceType = maintenance.ServiceType,
                CarName = maintenance.CarName,
                GarageName = maintenance.GarageName,
                ScheduledDate = maintenance.ScheduledDate,
                CarId = maintenance.CarId,
                GarageId = maintenance.GarageId
            };
        }

        public static Maintenance ConvertMaintenanceInPostDTOToMaintenance(this MaintenanceInPostDTO maintenanceInPostDTO)
        {
            return new Maintenance
            {
                ScheduledDate = DateOnly.Parse(maintenanceInPostDTO.ScheduledDate),
                ServiceType = maintenanceInPostDTO.ServiceType,
                GarageId = maintenanceInPostDTO.GarageId,
                CarId = maintenanceInPostDTO.CarId
            };
        }
    }
}
