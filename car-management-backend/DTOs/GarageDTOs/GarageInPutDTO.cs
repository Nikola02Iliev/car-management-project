using System.ComponentModel.DataAnnotations;

namespace car_management_backend.DTOs.GarageDTOs
{
    public class GarageInPutDTO
    {
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(50, ErrorMessage = "Name must be no more than 50 characters!")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required!")]
        [StringLength(50, ErrorMessage = "Location must be no more than 50 characters!")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required!")]
        [StringLength(50, ErrorMessage = "City must be no more than 50 characters!")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Capacity is required!")]
        [Range(0, 15, ErrorMessage = "Capacity must be between 0 and 15!")]
        public int? Capacity { get; set; }
    }
}
