using car_management_backend.DTOs.GarageDTOs;

namespace car_management_backend.DTOs.CarDTOs
{
    public class CarAfterPutResponseDTO
    {
        public int CarId { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? ProductionYear { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public List<GarageDTOInCarAfterPutResponseDTO> Garages { get; set; } = new List<GarageDTOInCarAfterPutResponseDTO>();
    }
}
