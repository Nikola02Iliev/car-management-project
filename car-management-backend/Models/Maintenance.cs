namespace car_management_backend.Models
{
    public class Maintenance
    {
        public int Id { get; set; }
        public string ServiceType { get; set; } = string.Empty;
        public DateTime ScheduledDate {  get; set; }
        public Car? Car { get; set; }
        public int? CarId { get; set; }
        public Garage? Garage { get; set; }
        public int? GarageId { get; set; }

    }
}
