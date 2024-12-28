using car_management_backend.DTOs.CarDTOs;
using car_management_backend.Models;

namespace car_management_backend.Mappers
{
    public static class CarMapper
    {
        public static CarInListDTO ConvertCarToCarInListDTO(this Car car)
        {
            return new CarInListDTO
            {
                Id = car.CarId,
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                Garages = car.CarGarages.Select(cg => cg.Garage.ConvertGarageToGarageDTOInCarInListDTO()).ToList()
            };

        }

        public static CarInGetDTO ConvertCarToCarInGetDTO(this Car car) 
        {
            return new CarInGetDTO
            {
                CarId = car.CarId,
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                Garages = car.CarGarages.Select(cg => cg.Garage.ConvertGarageToGarageDTOInCarInGetDTO()).ToList()
            };
        }

         

        public static Car ConvertCarInPostDTOToCar(this CarInPostDTO carInPostDTO)
        {
            return new Car
            {
                Make = carInPostDTO.Make,
                Model = carInPostDTO.Model,
                ProductionYear = carInPostDTO.ProductionYear,
                LicensePlate = carInPostDTO.LicensePlate
            };
        }

        public static Car ConvertCarInPutDTOToCar(this CarInPutDTO carInPutDTO)
        {
            return new Car
            {
                Make = carInPutDTO.Make,
                Model = carInPutDTO.Model,
                ProductionYear = carInPutDTO.ProductionYear,
                LicensePlate = carInPutDTO.LicensePlate
            };
        }


    }
}
