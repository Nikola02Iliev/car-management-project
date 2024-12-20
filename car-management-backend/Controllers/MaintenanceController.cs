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

namespace car_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
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
        public async Task<ActionResult<Maintenance>> GetMaintenance(int maintenanceId)
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
        public async Task<IActionResult> PutMaintenance(int maintenanceId, MaintenanceInPutDTO maintenanceInPutDTO)
        {
            var maintenance = await _maintenanceService.GetMaintenanceByIdAsync(maintenanceId);
            if (maintenance == null)
            {
                return NotFound($"No maintenance found with id {maintenanceId}!");
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
        public async Task<ActionResult<MaintenanceAfterPostResponseDTO>> PostMaintenance(MaintenanceInPostDTO maintenanceInPostDTO)
        {
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
        public async Task<IActionResult> DeleteMaintenance(int maintenanceId)
        {
            var maintenance = await _maintenanceService.GetMaintenanceByIdAsync(maintenanceId);

            if(maintenance == null)
            {
                return NotFound($"No maintenance found with id {maintenanceId}!");
            }

            await _maintenanceService.DeleteMaintenanceAsync(maintenance);

            return NoContent();
        }

    }
}
