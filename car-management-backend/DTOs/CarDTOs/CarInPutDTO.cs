namespace car_management_backend.DTOs.CarDTOs
{
    public class CarInPutDTO
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? ProductionYear { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public List<int> GaragesIds { get; set; } = new List<int>();
    }
}
