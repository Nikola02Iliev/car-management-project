﻿using System;
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
using car_management_backend.DTOs.Garage;

namespace car_management_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly IGarageService _garageService;

        public GarageController(IGarageService garageService)
        {
            _garageService = garageService;
        }



        //Get All Garages
        [HttpGet]
        public IActionResult GetGarages()
        {
            var garages = _garageService.GetGarages();

            var garagesInListDTO = garages.Select(g => g.ConvertGarageToGarageInListDTO());

            return Ok(garagesInListDTO);
        }

        //Get Garage By Id
        [HttpGet("{garageId}")]
        public async Task<ActionResult<GarageInGetDTO?>> GetGarageByIdAsync(int garageId)
        {
            var garage = await _garageService.GetGarageByIdAsync(garageId);

            if (garage == null) 
            {
                return NotFound($"No garage found with id {garageId}!");
            }

            var garagesInGetDTO = garage.ConvertGarageToGarageInGetDTO();

            return Ok(garagesInGetDTO);
        }

        //Update Garage
        [HttpPut("{garageId}")]
        public async Task<IActionResult> PutGarage(int garageId, GarageInPutDTO garageInPutDTO)
        {
            var garage = await _garageService.GetGarageByIdAsync(garageId);

            if (garage == null)
            {
                return NotFound($"No garage found with id {garageId}!");
            }

            await _garageService.UpdateGarageAsync(garage, garageInPutDTO);

            
            return NoContent();
        }

        //Create Garage
        [HttpPost]
        public async Task<ActionResult<Garage>> PostGarageAsync(GarageInPostDTO garageInPostDTO)
        {
            var garage = garageInPostDTO.ConvertGarageInPostDTOToGarage();

            await _garageService.CreateGarageAsync(garage);

            
            return Ok(new GarageAfterPostResponseDTO
            {
                GarageId = garage.GarageId,
                Name = garage.Name,
                City = garage.City,
                Location = garage.Location,
                Capacity = garage.Capacity
            });
        }

        //Delete Garage
        [HttpDelete("{garageId}")]
        public async Task<IActionResult> DeleteGarage(int garageId)
        {
            var garage = await _garageService.GetGarageByIdAsync(garageId);
            if (garage == null) 
            {
                return NotFound($"No garage found with id {garageId}!");
            }

            await _garageService.DeleteGarageAsync(garage);

            return NoContent();
        }


    }
}