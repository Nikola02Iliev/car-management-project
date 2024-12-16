using car_management_backend.DTOs.Garage;
using car_management_backend.Models;

namespace car_management_backend.Mappers
{
    public static class GarageMapper
    {
        public static GarageInListDTO ConvertGarageToGarageInListDTO(this Garage garage)
        {
            return new GarageInListDTO
            {
                GarageId = garage.GarageId,
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

        public static GarageInGetDTO ConvertGarageToGarageInGetDTO(this Garage garage) 
        {
            return new GarageInGetDTO
            {
                GarageId = garage.GarageId,
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

        public static Garage ConvertGarageInPostDTOToGarage(this GarageInPostDTO garageInPostDTO) 
        {
            return new Garage
            {
                Name = garageInPostDTO.Name,
                Location = garageInPostDTO.Location,
                City = garageInPostDTO.City,
                Capacity= garageInPostDTO.Capacity
            };
        }

    }
}
