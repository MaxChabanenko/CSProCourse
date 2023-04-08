using Logistic.Models;

namespace Logistic.Core
{
    public class WarehouseService : IService<Warehouse, int>
    {
        private IRepository<Warehouse> _warehouseRepository;
        public WarehouseService(IRepository<Warehouse> warehouseRepository)
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
            if (cargo is null)
            {
                throw new Exception("Cargo not found");
            }
            warehouse.Cargos.Remove(cargo);

            _warehouseRepository.Update(warehouse.Id, warehouse);

            return cargo.CloneJson();
        }
    }
}
