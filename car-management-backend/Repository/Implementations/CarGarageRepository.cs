using car_management_backend.Context;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace car_management_backend.Repository.Implementations
{
    public class CarGarageRepository : ICarGarageRepository
    {
        private readonly AppDBContext _context;
        private readonly DbSet<CarGarage> _dbSet;

        public CarGarageRepository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<CarGarage>();
        }

        

        public async Task<List<CarGarage>> GetCarGaragesForCar(int carId)
        {
            var carGarages = await _dbSet.Where(cg => cg.CarId == carId).ToListAsync();

            return carGarages;
        }

        public async Task<List<CarGarage>> GetCarGaragesForGarage(int garageId)
        {
            var carGarages = await _dbSet.Where(cg => cg.GarageId == garageId).ToListAsync();

            return carGarages;

        }

        public void DeleteCarGarages(List<CarGarage> carGarages)
        {
            _dbSet.RemoveRange(carGarages);
        }

        public async Task<List<int?>> GetGarageIdsForCar(int carId)
        {
            var garageIds = await _dbSet.Where(cg => cg.CarId == carId).Select(g => g.GarageId).ToListAsync();

            return garageIds;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
