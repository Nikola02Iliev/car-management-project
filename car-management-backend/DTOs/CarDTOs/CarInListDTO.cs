﻿using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;

namespace car_management_backend.DTOs.CarDTOs
{
    public class CarInListDTO
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? ProductionYear { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public List<GarageDTOInCarInListDTO> Garages { get; set; } = new List<GarageDTOInCarInListDTO>();
    }
}
