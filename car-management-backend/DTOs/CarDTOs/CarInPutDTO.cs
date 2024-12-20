using System.ComponentModel.DataAnnotations;

namespace car_management_backend.DTOs.CarDTOs
{
    public class CarInPutDTO
    {
        [Required(ErrorMessage = "Make is required!")]
        [StringLength(50, ErrorMessage = "Make must be no more than 50 characters!")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required!")]
        [StringLength(50, ErrorMessage = "Model must be no more than 50 characters!")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Production year is required!")]
        [Range(0, 15, ErrorMessage = "Production year must be between 0 and 15!")]
        public int? ProductionYear { get; set; }

        [Required(ErrorMessage = "License plate is required!")]
        [StringLength(50, ErrorMessage = "License plate must be no more than 50 characters!")]
        public string LicensePlate { get; set; } = string.Empty;

        [Required(ErrorMessage = "GaragesIds is required!")]
        public List<int> GaragesIds { get; set; } = new List<int>();
    }
}
