namespace car_management_backend.DTOs.MaintenanceDTOs
{
    public class MaintenanceInPostDTO
    {
        public DateTime ScheduledDate { get; set; }
        public string ServiceType {  get; set; } = string.Empty;
        public int GarageId {  get; set; }
        public int CarId { get; set; }
    }
}
