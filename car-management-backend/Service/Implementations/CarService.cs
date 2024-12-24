using car_management_backend.DTOs.CarDTOs;
using car_management_backend.Models;
using car_management_backend.Queries;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly ICarGarageRepository _carGarageRepository;
        private IMaintenanceRepository _maintenanceRepository;

        public CarService(ICarRepository carRepository, IGarageRepository garageRepository, ICarGarageRepository carGarageRepository, IMaintenanceRepository maintenanceRepository)
        {
            _carRepository = carRepository;
            _garageRepository = garageRepository;
            _carGarageRepository = carGarageRepository;
            _maintenanceRepository = maintenanceRepository;
        }

        public IQueryable<Car> GetCars([FromQuery] CarQueries carQueries) 
        {
            var cars = _carRepository.GetCars();

            if (!string.IsNullOrWhiteSpace(carQueries.Make))
            {
                cars = cars.Where(c => c.Make == carQueries.Make);
            }

            if (!string.IsNullOrWhiteSpace(carQueries.Garage?.Name))
            {
                cars = cars.Where(c => c.CarGarages.Any(cg => cg.Garage.Name == carQueries.Garage.Name));
            }

            if (carQueries.FromYear != null)
            {
                cars = cars.Where(c => c.ProductionYear >= carQueries.FromYear);
            }

            if (carQueries.ToYear != null)
            {
                cars = cars.Where(c => c.ProductionYear <= carQueries.ToYear);
            }

            return cars;
        }

        public async Task<Car?> GetCarByIdAsync(int? carId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);

            return car;
        }

        public async Task CreateCarAsync(Car car, List<int> garageIds)
        {
            var garages = _garageRepository.GetGarages().Where(g => garageIds.Contains(g.GarageId)).ToList();

            foreach (var garage in garages)
            {
                var carGarage = new CarGarage
                {
                    Car = car,
                    Garage = garage
                };

                car.CarGarages.Add(carGarage);
            }

            await _carRepository.CreateCarAsync(car);

            await _carRepository.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car, CarInPutDTO carInPutDTO, List<int> garageIds)
        {
            var currentCarGarages = car.CarGarages.ToList();

            foreach (var carGarage in currentCarGarages)
            {
                if (!garageIds.Contains(carGarage.Garage.GarageId))
                {
                    car.CarGarages.Remove(carGarage);
                }
            }

            var garages = _garageRepository.GetGarages().Where(g => garageIds.Contains(g.GarageId)).ToList();

            foreach (var garage in garages)
            {
                var carGarage = new CarGarage
                {
                    Car = car,
                    Garage = garage
                };

                car.CarGarages.Add(carGarage);
            }

            _carRepository.UpdateCar(car, carInPutDTO);

            await _carRepository.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(Car car)
        {
            var carGarages = await _carGarageRepository.GetCarGaragesForCar(car.CarId);
            var maintenances = await _maintenanceRepository.GetMaintenancesForCar(car.CarId);

            if (maintenances.Any())
            {
                _maintenanceRepository.DeleteMaintenances(maintenances);
                await _maintenanceRepository.SaveChangesAsync();
            }

            if (carGarages.Any())
            {
                _carGarageRepository.DeleteCarGarages(carGarages);
                await _carGarageRepository.SaveChangesAsync(); 
            }

            _carRepository.DeleteCar(car);
            await _carRepository.SaveChangesAsync();
        }

        public async Task<List<Car>> GetCarsInGarageAsync(int garageId)
        {
            var carsInGarage = await _carGarageRepository.GetCarsInGarage(garageId);

            return carsInGarage;
        }

        public async Task<List<int?>> GetCarIdsInMaintenanceInGarage(int garageId)
        {
            var carIds = await _maintenanceRepository.GetMaintenances().Where(m => m.GarageId == garageId).Select(m => m.CarId).ToListAsync();

            return carIds;
        }

        
    }
}
