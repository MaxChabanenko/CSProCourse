﻿using Logistic.Models;

namespace Logistic.Core
{
    public class VehicleService : IService<Vehicle, int>
    {
        private IRepository<Vehicle> _vehicleRepository;
        public VehicleService(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public void LoadCargo(Cargo cargoToLoad, int vehicleId)
        {
            if (cargoToLoad.Volume <= 0)
                throw new ArgumentException("Invalid Cargo parameter: ", nameof(cargoToLoad.Volume));
            if (cargoToLoad.Weight <= 0)
                throw new ArgumentException("Invalid Cargo parameter: ", nameof(cargoToLoad.Weight));


            var vehicleToUpdate = _vehicleRepository.ReadById(vehicleId);

            var totalWeight = vehicleToUpdate.Cargos.Sum(x => x.Weight) + cargoToLoad.Weight;
            if (totalWeight > vehicleToUpdate.MaxCargoWeightKg)
            {
                throw new ArgumentException("Not enough weight capacity to fit in the " + cargoToLoad.ToString());
            }

            var totalVolume = vehicleToUpdate.Cargos.Sum(x => x.Volume) + cargoToLoad.Volume;
            if (totalVolume > vehicleToUpdate.MaxCargoVolume)
            {
                throw new ArgumentException("Not enough space to fit in the " + cargoToLoad.ToString());
            }

            vehicleToUpdate.Cargos.Add(cargoToLoad);
            _vehicleRepository.Update(vehicleToUpdate.Id, vehicleToUpdate);
        }

        public Cargo UnloadCargo(Guid cargoId, int vehicleId)
        {
            var vehicleToUpdate = _vehicleRepository.ReadById(vehicleId);

            Cargo cargo = vehicleToUpdate.Cargos.Find(x => x.Id == cargoId);
            if (cargo is null)
            {
                throw new ArgumentException("Cargo not found");
            }
            vehicleToUpdate.Cargos.Remove(cargo);

            _vehicleRepository.Update(vehicleToUpdate.Id, vehicleToUpdate);
            return cargo.CloneJson();
        }

        public int Create(Vehicle vehicle)
        {
            return _vehicleRepository.Create(vehicle);
        }

        public void Delete(int id)
        {
            _vehicleRepository.Delete(id);
        }
        public int Update(Vehicle newVehicle, int deleteId)
        {
            _vehicleRepository.Delete(deleteId);
            return _vehicleRepository.Create(newVehicle);
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
