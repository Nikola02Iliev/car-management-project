namespace car_management_backend.Models
{
    public class CarGarage
    {
        public int CarGarageId { get; set; }
        public int? CarId { get; set; }
        public Car? Car { get; set; }
        public int? GarageId { get; set; }
        public Garage? Garage { get; set; }
    }
}
