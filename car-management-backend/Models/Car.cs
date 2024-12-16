namespace car_management_backend.Models
{
    public class Car
    {
        public int CarId {  get; set; }
        public string Make {  get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? ProductionYear { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public List<Garage> Garages { get; set; } = new List<Garage>();
    }
}
