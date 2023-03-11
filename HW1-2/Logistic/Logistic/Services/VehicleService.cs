using Logistic.ConsoleClient.Classes;
using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Services
{
    internal class VehicleService : IService<Vehicle>
    {
        private int currentWeight { get; set; }
        private double currentVolume { get; set; }
        public void Create(Vehicle vehicle)
        {
            InMemeoryRepository<Vehicle, int>.Create(vehicle);
        }

        public void Delete(int id)
        {
            InMemeoryRepository<Vehicle, int>.Delete(id);
        }

        public List<Vehicle> GetAll()
        {
            return InMemeoryRepository<Vehicle, int>.ReadAll();
        }

        public Vehicle GetById(int id)
        {
            return InMemeoryRepository<Vehicle, int>.ReadById(id);
        }

        public void LoadCargo(Cargo cargo, int id)
        {
            bool overWeight = currentWeight + cargo.Weight > GetById(id).MaxCargoWeightKg;
            bool overVolume = currentVolume + cargo.Volume > GetById(id).MaxCargoVolume;
            if (overWeight)
            {
                throw new Exception("Not enough weight capacity to fit in the " + cargo.ToString());
            }
            if (overVolume)
            {
                throw new Exception("Not enough space to fit in the " + cargo.ToString());
            }


            this.currentWeight += cargo.Weight;
            this.currentVolume += cargo.Volume;

            var vehicle = GetById(id);
            vehicle.Add(cargo);

        }


        public Cargo UnloadCargo(Guid cargoId, int id)
        {

            var vehicle = GetById(id);
            var cargo = vehicle.DeleteByGuid(cargoId);
            if (cargo != null)
            {
                this.currentWeight -= cargo.Weight;
                this.currentVolume -= cargo.Volume;
            }
            return cargo;
        }
    }
}
