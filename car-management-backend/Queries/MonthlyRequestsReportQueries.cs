using System.ComponentModel.DataAnnotations;

namespace car_management_backend.Queries
{
    public class MonthlyRequestsReportQueries
    {
        [Required]
        public int GarageId { get; set; }

        [Required]
        public string StartMonth {  get; set; } = string.Empty;

        [Required]
        public string EndMonth { get; set; } = string.Empty;
    }
}
