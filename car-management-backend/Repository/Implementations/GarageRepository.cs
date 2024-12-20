using car_management_backend.Context;
using car_management_backend.DTOs.GarageDTOs;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend.Repository.Implementations
{
    public class GarageRepository : IGarageRepository
    {
        private readonly AppDBContext _context;
        private readonly DbSet<Garage> _dbSet;

        public GarageRepository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<Garage>();
        }

        public IQueryable<Garage> GetGarages()
        {
            var garages = _dbSet.AsQueryable();

            return garages;
        }

        public async Task<Garage?> GetGarageByIdAsync(int? garageId)
        {
            var garage = await _dbSet.SingleOrDefaultAsync(g => g.GarageId == garageId);

            return garage;
        }

        public async Task CreateGarageAsync(Garage garage)
        {
            await _dbSet.AddAsync(garage);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateGarage(Garage garage, GarageInPutDTO garageInPutDTO)
        {
            garage.Name = garageInPutDTO.Name;
            garage.Location = garageInPutDTO.Location;
            garage.City = garageInPutDTO.City;
            garage.Capacity = garageInPutDTO.Capacity;
        }

        public void DeleteGarage(Garage garage)
        {
            _dbSet.Remove(garage);
        }
    }
}
