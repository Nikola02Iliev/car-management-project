﻿namespace car_management_backend.Models
{
    public class Garage
    {
        public int GarageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int? Capacity {  get; set; }
        public List<CarGarage> CarGarages { get; set; } = new List<CarGarage>();


    }
}
