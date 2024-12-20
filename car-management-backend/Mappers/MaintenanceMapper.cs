using car_management_backend.DTOs.MaintenanceDTOs;
using car_management_backend.Models;

namespace car_management_backend.Mappers
{
    public static class MaintenanceMapper
    {
        public static Maintenance ConvertMaintenanceInPostDTOToMaintenance(this MaintenanceInPostDTO maintenanceInPostDTO)
        {
            return new Maintenance
            {
                ScheduledDate = maintenanceInPostDTO.ScheduledDate,
                ServiceType = maintenanceInPostDTO.ServiceType,
                GarageId = maintenanceInPostDTO.GarageId,
                CarId = maintenanceInPostDTO.CarId
            };
        }
    }
}
