namespace car_management_backend.DTOs.MaintenanceDTOs
{
    public class MaintenanceInListDTO
    {
        public int Id { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public string CarName { get; set; } = string.Empty;
        public string GarageName { get; set; } = string.Empty;
        public DateOnly ScheduledDate { get; set; }
        public int? CarId { get; set; }
        public int? GarageId { get; set; }
    }
}
