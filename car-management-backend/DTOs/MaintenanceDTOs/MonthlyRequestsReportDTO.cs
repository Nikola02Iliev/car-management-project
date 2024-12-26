namespace car_management_backend.DTOs.MaintenanceDTOs
{
    public class MonthlyRequestsReportDTO
    {
        public YearMonthDTO? YearMonth { get; set; }
        public int? Requests { get; set; }

    }
}
