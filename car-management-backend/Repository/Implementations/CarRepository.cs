using car_management_backend.Context;
using car_management_backend.DTOs.CarDTOs;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend.Repository.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDBContext _context;
        private readonly DbSet<Car> _dbSet;

        public CarRepository(AppDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<Car>();
        }

        public IQueryable<Car> GetCars()
        {
            var cars = _dbSet
                .Include(c=>c.CarGarages)
                    .ThenInclude(cg=>cg.Garage)
                .AsQueryable();

            return cars;
        }

        public async Task<Car?> GetCarByIdAsync(int carId)
        {
            var car = await _dbSet
                .Include(c=>c.CarGarages)
                    .ThenInclude(cg=>cg.Garage)
                .SingleOrDefaultAsync(c => c.CarId == carId);

            return car;
        }

        public async Task CreateCarAsync(Car car)
        {
            await _dbSet.AddAsync(car);
        }

        public void UpdateCar(Car car, CarInPutDTO carInPutDTO)
        {
            car.Make = carInPutDTO.Make;
            car.Model = carInPutDTO.Model;
            car.ProductionYear = carInPutDTO.ProductionYear;
            car.LicensePlate = carInPutDTO.LicensePlate;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void DeleteCar(Car car)
        {
            _dbSet.Remove(car);
        }
    }
}
