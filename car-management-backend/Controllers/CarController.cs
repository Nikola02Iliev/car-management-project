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

namespace car_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        //Get All Cars
        [HttpGet]
        public IActionResult GetCars()
        {
            var cars = _carService.GetCars();

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
        public async Task<ActionResult<CarAfterPutResponseDTO>> PutCar(int carId, CarInPutDTO carInPutDTO)
        {
            var garageIds = carInPutDTO.GaragesIds;

            var car = await _carService.GetCarByIdAsync(carId);
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
        public async Task<ActionResult<CarAfterPostResponseDTO>> PostCar(CarInPostDTO carInPostDTO)
        {
            var garageIds = carInPostDTO.GaragesIds;
            
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
        public async Task<ActionResult> DeleteCar(int carId)
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
