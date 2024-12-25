namespace car_management_backend.DTOs.GarageDTOs
{
    public class GarageReportDTO
    {
        public string Date {  get; set; } = string.Empty;
        public int? Requests { get; set; }
        public int? AvailableCapacity { get; set; }
    }
}
