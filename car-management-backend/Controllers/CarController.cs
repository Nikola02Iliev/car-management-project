using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using car_management_backend.Context;
using car_management_backend.Models;
using car_management_backend.Service.Interfaces;
using car_management_backend.Mappers;
using car_management_backend.DTOs.CarDTOs;
using car_management_backend.Queries;

namespace car_management_backend.Controllers
{
    [Route("cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IGarageService _garageService;

        public CarController(ICarService carService, IGarageService garageService)
        {
            _carService = carService;
            _garageService = garageService;
        }

        //Get All Cars
        [HttpGet]
        public IActionResult GetCars([FromQuery] CarQueries carQueries)
        {
            var cars = _carService.GetCars(carQueries);

            var carsInListDTO = cars.Select(c => c.ConvertCarToCarInListDTO());

            return Ok(carsInListDTO);
        }

        //Get Car By Id
        [HttpGet("{carId}")]
        public async Task<ActionResult<CarInGetDTO?>> GetCarByIdAsync(int? carId)
        {
            var car = await _carService.GetCarByIdAsync(carId);

            if (car == null) 
            {
                return NotFound($"No car found with id {carId}!");
            }

            var carInGetDTO = car.ConvertCarToCarInGetDTO();
            
            return Ok(carInGetDTO);
        }

        //Update Car By Id
        [HttpPut("{carId}")]
        public async Task<ActionResult<CarAfterPutResponseDTO>> PutCarAsync(int carId, CarInPutDTO carInPutDTO)
        {
            var garageIds = carInPutDTO.GaragesIds;

            var car = await _carService.GetCarByIdAsync(carId);

            var currentGarageIds = await _garageService.GetAllGarageIdsAsyncForCar(carId);

            if (car == null)
            {
                return NotFound($"No car found with id {carId}!");
            }

            await _carService.UpdateCarAsync(car, carInPutDTO, garageIds);
            
            return Ok(new CarAfterPutResponseDTO
            {
                CarId = car.CarId,
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                Garages = car.CarGarages.Select(cg => cg.Garage.ConvertGarageToGarageDTOInCarAfterPutResponseDTO()).ToList()
            });
        }

        //Create Car
        [HttpPost]
        public async Task<ActionResult<CarAfterPostResponseDTO>> PostCarAsync(CarInPostDTO carInPostDTO)
        {
            var garageIds = carInPostDTO.GaragesIds;

            var allGarageIds = await _garageService.GetAllGarageIdsAsync();

            foreach(var garageId in garageIds)
            {
                if (!allGarageIds.Contains(garageId))
                {
                    return NotFound($"No garage found with id {garageId}!");

                }
            }

            var car = carInPostDTO.ConvertCarInPostDTOToCar();

            await _carService.CreateCarAsync(car, garageIds);

            return Ok(new CarAfterPostResponseDTO
            {
                CarId = car.CarId,
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                Garages = car.CarGarages.Select(cg => cg.Garage.ConvertGarageToGarageDTOInCarAfterPostResponseDTO()).ToList()

            });
        }

        //Delete Car By Id
        [HttpDelete("{carId}")]
        public async Task<ActionResult> DeleteCarAsync(int carId)
        {
            var car = await _carService.GetCarByIdAsync(carId);
            if (car == null)
            {
                return NotFound();
            }

            await _carService.DeleteCarAsync(car);

            return Ok(true);
        }


    }
}
