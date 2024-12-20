using car_management_backend.Models;
using Newtonsoft.Json.Bson;

namespace car_management_backend.Repository.Interfaces
{
    public interface ICarGarageRepository
    {
        Task<List<CarGarage>> GetCarGaragesForCar(int carId);
        Task<List<CarGarage>> GetCarGaragesForGarage(int garageId);
        void DeleteCarGarages(List<CarGarage> carGarages);
        Task<List<int?>> GetGarageIdsForCar(int carId);
        Task SaveChangesAsync();
    }
}
