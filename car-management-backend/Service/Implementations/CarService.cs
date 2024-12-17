using car_management_backend.DTOs.CarDTOs;
using car_management_backend.Models;
using car_management_backend.Repository.Interfaces;
using car_management_backend.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace car_management_backend.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IGarageRepository _garageRepository;
        private readonly ICarGarageRepository _carGarageRepository;

        public CarService(ICarRepository carRepository, IGarageRepository garageRepository, ICarGarageRepository carGarageRepository)
        {
            _carRepository = carRepository;
            _garageRepository = garageRepository;
            _carGarageRepository = carGarageRepository;
        }

        public IQueryable<Car> GetCars() 
        {
            var cars = _carRepository.GetCars();

            return cars;
        }

        public async Task<Car?> GetCarByIdAsync(int carId)
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

            if (carGarages.Any())
            {
                _carGarageRepository.DeleteCarGarages(carGarages);
                await _carGarageRepository.SaveChangesAsync(); 
            }

            _carRepository.DeleteCar(car);
            await _carRepository.SaveChangesAsync();
        }
    }
}
