﻿namespace car_management_backend.DTOs.GarageDTOs
{
    public class GarageDTOInCarAfterPostResponseDTO
    {
        public int GarageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int? Capacity { get; set; }
    }
}
