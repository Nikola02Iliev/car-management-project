using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace car_management_backend.DTOs.MaintenanceDTOs
{
    public class MaintenanceInPutDTO
    {
        [DefaultValue("yyyy-MM-dd")]
        public string ScheduledDate { get; set; }

        [Required(ErrorMessage = "Service type is required!")]
        [StringLength(50, ErrorMessage = "Service type must be no more than 50 characters!")]
        public string ServiceType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Garage id is required!")]
        public int GarageId { get; set; }

        [Required(ErrorMessage = "Car id is required!")]
        public int CarId { get; set; }
    }
}
