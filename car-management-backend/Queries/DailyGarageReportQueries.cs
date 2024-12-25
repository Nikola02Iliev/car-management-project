using System.ComponentModel.DataAnnotations;

namespace car_management_backend.Queries
{
    public class DailyGarageReportQueries
    {
        [Required]
        public int GarageId { get; set; }

        [Required]
        public string StartDate { get; set; } = string.Empty;

        [Required]
        public string EndDate { get; set; } = string.Empty;
    }
}
