using Logistic.ConsoleClient.Classes;
using Logistic.ConsoleClient.DataAccess;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.Services
{
    internal class WarehouseService : IService<Warehouse,int>
    {
        private InMemoryRepository<Warehouse> _warehouseRepository;
        public WarehouseService(InMemoryRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public int Create(Warehouse warehouse)
        {
          return _warehouseRepository.Create(warehouse);
        }

        public void Delete(int id)
        {
            _warehouseRepository.Delete(id);
        }

        public List<Warehouse> GetAll()
        {
            return _warehouseRepository.ReadAll();
        }

        public Warehouse GetById(int id)
        {
            return _warehouseRepository.ReadById(id);
        }

        public void LoadCargo(Cargo cargo, int id)
        {
            var warehouse = GetById(id);
            warehouse.Cargos.Add(cargo);
            _warehouseRepository.Update(warehouse.Id, warehouse);
        }

        public Cargo UnloadCargo(Guid cargoId, int id)
        {
            var warehouse = GetById(id);

            Cargo cargo = warehouse.Cargos.Find(x => x.Id == cargoId);
            warehouse.Cargos.Remove(cargo);

            _warehouseRepository.Update(warehouse.Id, warehouse);

            return cargo.CloneJson();
        }
    }
}
