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
using car_management_backend.DTOs.MaintenanceDTOs;
using Microsoft.CodeAnalysis.Operations;
using car_management_backend.Mappers;
using System.Globalization;

namespace car_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;
        private readonly IGarageService _garageService;
        private readonly ICarService _carService;
        public MaintenanceController(IMaintenanceService maintenanceService, IGarageService garageService, ICarService carService)
        {
            _maintenanceService = maintenanceService;
            _garageService = garageService;
            _carService = carService;
        }

        //Get All Maintenances
        [HttpGet]
        public IActionResult GetMaintenances()
        {
            var maintenances = _maintenanceService.GetMaintenances();

            return Ok(maintenances);
        }

        //Get Maintenance By Id
        [HttpGet("{maintenanceId}")]
        public async Task<ActionResult<Maintenance>> GetMaintenanceByIdAsync(int maintenanceId)
        {
            var maintenance = await _maintenanceService.GetMaintenanceByIdAsync(maintenanceId);

            if (maintenance == null)
            {
                return NotFound($"No maintenance found with id {maintenanceId}!");
            }

            return Ok(new MaintenanceInGetDTO
            {
                MaintenanceId = maintenance.MaintenanceId,
                ServiceType = maintenance.ServiceType,
                CarName = maintenance.CarName,
                GarageName = maintenance.GarageName,
                ScheduledDate = maintenance.ScheduledDate,
                CarId = maintenance.CarId,
                GarageId = maintenance.GarageId
            });
        }

        //Update Maintenance By Id
        [HttpPut("{maintenanceId}")]
        public async Task<IActionResult> PutMaintenanceAsync(int maintenanceId, MaintenanceInPutDTO maintenanceInPutDTO)
        {
            var maintenance = await _maintenanceService.GetMaintenanceByIdAsync(maintenanceId);
            if (maintenance == null)
            {
                return NotFound($"No maintenance found with id {maintenanceId}!");
            }

            var garage = await _garageService.GetGarageByIdAsync(maintenanceInPutDTO.GarageId);

            if (garage == null)
            {
                return NotFound($"No garage found with id {maintenanceInPutDTO.GarageId}!");
            }

            var car = await _carService.GetCarByIdAsync(maintenanceInPutDTO.CarId);

            var carsInGarage = await _carService.GetCarsInGarageAsync(garage.GarageId);

            if (!carsInGarage.Contains(car))
            {
                return NotFound($"No car with id {maintenanceInPutDTO.CarId} found in garage with id {maintenanceInPutDTO.GarageId}!");

            }

            List<DateOnly> allScheduledDatesInGarage = await _maintenanceService.GetAllScheduledDatesInGarage(garage.GarageId);
            string format = "yyyy-MM-dd";
            DateOnly dateOnly = new DateOnly();
            DateTime dateTimeNow = DateTime.Now;
            DateOnly dateOnlyToday = DateOnly.FromDateTime(dateTimeNow);

            bool IsValidFormat = DateOnly.TryParseExact(maintenanceInPutDTO.ScheduledDate,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateOnly);

            if (!IsValidFormat)
            {
                return BadRequest("Invalid schedule date format!");
            }

            if (dateOnlyToday > dateOnly)
            {
                return BadRequest("You can't request maintenance before today!");
            }

            if (allScheduledDatesInGarage.Contains(dateOnly))
            {
                return BadRequest("This schedule date is already reserved!");
            }

            await _maintenanceService.UpdateMaintenanceAsync(maintenance, maintenanceInPutDTO);

            return Ok(new MaintenanceAfterPutResponseDTO
            {
                MaintenanceId = maintenance.MaintenanceId,
                CarName = maintenance.CarName,
                GarageName = maintenance.GarageName,
                ScheduledDate = maintenance.ScheduledDate,
                ServiceType = maintenance.ServiceType,
                GarageId = maintenance.GarageId,
                CarId = maintenance.CarId

            });
        }

        //Create Maintenance
        [HttpPost]
        public async Task<ActionResult<MaintenanceAfterPostResponseDTO>> PostMaintenanceAsync(MaintenanceInPostDTO maintenanceInPostDTO)
        {
            var garage = await _garageService.GetGarageByIdAsync(maintenanceInPostDTO.GarageId);

            if(garage == null)
            {
                return NotFound($"No garage found with id {maintenanceInPostDTO.GarageId}!");
            }

            var car = await _carService.GetCarByIdAsync(maintenanceInPostDTO.CarId);

            var carsInGarage = await _carService.GetCarsInGarageAsync(garage.GarageId);

            if (!carsInGarage.Contains(car))
            {
                return NotFound($"No car with id {maintenanceInPostDTO.CarId} found in garage with id {maintenanceInPostDTO.GarageId}!");

            }

            List<DateOnly> allScheduledDatesInGarage = await _maintenanceService.GetAllScheduledDatesInGarage(garage.GarageId);
            string format = "yyyy-MM-dd";
            DateOnly dateOnly = new DateOnly();
            DateTime dateTimeNow = DateTime.Now;
            DateOnly dateOnlyToday = DateOnly.FromDateTime(dateTimeNow);

            bool IsValidFormat = DateOnly.TryParseExact(maintenanceInPostDTO.ScheduledDate,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateOnly);

            if (!IsValidFormat)
            {
                return BadRequest("Invalid schedule date format!");
            }

            if (dateOnlyToday > dateOnly)
            {
                return BadRequest("You can't request maintenance before today!");
            }

            if (allScheduledDatesInGarage.Contains(dateOnly))
            {
                return BadRequest("This schedule date is already reserved!");
            }


            var maintenance = maintenanceInPostDTO.ConvertMaintenanceInPostDTOToMaintenance();

            await _maintenanceService.CreateMaintenanceAsync(maintenance);


            return Ok(new MaintenanceAfterPostResponseDTO
            {
                MaintenanceId = maintenance.MaintenanceId,
                ServiceType = maintenance.ServiceType,
                CarName = maintenance.CarName,
                GarageName = maintenance.GarageName,
                ScheduledDate = maintenance.ScheduledDate,
                CarId = maintenance.CarId,
                GarageId= maintenance.GarageId
            });
        }

        //Delete Maintenance By Id
        [HttpDelete("{maintenanceId}")]
        public async Task<ActionResult> DeleteMaintenanceAsync(int maintenanceId)
        {
            var maintenance = await _maintenanceService.GetMaintenanceByIdAsync(maintenanceId);

            if(maintenance == null)
            {
                return NotFound($"No maintenance found with id {maintenanceId}!");
            }

            await _maintenanceService.DeleteMaintenanceAsync(maintenance);

            return Ok(true);
        }

    }
}
