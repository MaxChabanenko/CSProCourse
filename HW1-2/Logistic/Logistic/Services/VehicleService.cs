using Logistic.ConsoleClient.Classes;
using Logistic.ConsoleClient.DataAccess;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.Services
{
    internal class VehicleService : IService<Vehicle,int>
    {
        private InMemoryRepository<Vehicle> _vehicleRepository;
        public VehicleService(InMemoryRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public void LoadCargo( Cargo cargoToLoad, int vehicleId)
        {
            var vehicleToUpdate = _vehicleRepository.ReadById(vehicleId);

            var totalWeight = vehicleToUpdate.Cargos.Sum(x => x.Weight) + cargoToLoad.Weight;
            if (totalWeight > vehicleToUpdate.MaxCargoWeightKg)
            {
                throw new Exception("Not enough weight capacity to fit in the " + cargoToLoad.ToString());
            }

            var totalVolume = vehicleToUpdate.Cargos.Sum(x => x.Volume) + cargoToLoad.Volume;
            if (totalVolume > vehicleToUpdate.MaxCargoVolume)
            {
                throw new Exception("Not enough space to fit in the " + cargoToLoad.ToString());
            }
            //здається, пряме звернення до списку порушує інкапсуляцію, але просили винести методи модифікації списку з класу Vehicle
            //до того ж, якщо список буде приватним, то його не можна буде серіалізувати
            vehicleToUpdate.Cargos.Add(cargoToLoad);
            _vehicleRepository.Update(vehicleToUpdate.Id, vehicleToUpdate);
        }
        public Cargo UnloadCargo(Guid cargoId, int vehicleId)
        {
            var vehicleToUpdate = _vehicleRepository.ReadById(vehicleId);

            Cargo cargo = vehicleToUpdate.Cargos.Find(x => x.Id == cargoId);
            vehicleToUpdate.Cargos.Remove(cargo);

            _vehicleRepository.Update(vehicleToUpdate.Id, vehicleToUpdate);
            return cargo.CloneJson();
        }

        public void Create(Vehicle vehicle)
        {
            _vehicleRepository.Create(vehicle);
        }

        public void Delete(int id)
        {
            _vehicleRepository.Delete(id);
        }

        public List<Vehicle> GetAll()
        {
            return _vehicleRepository.ReadAll();
        }

        public Vehicle GetById(int id)
        {
            return _vehicleRepository.ReadById(id);
        }

        
    }
}
