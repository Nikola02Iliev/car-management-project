namespace car_management_backend.DTOs.MaintenanceDTOs
{
    public class YearMonthDTO
    {
        public int? Year { get; set; }
        public string Month { get; set; } = string.Empty;
        public bool LeapYear { get; set; } = false;
        public int? MonthValue { get; set; }
    }
}
