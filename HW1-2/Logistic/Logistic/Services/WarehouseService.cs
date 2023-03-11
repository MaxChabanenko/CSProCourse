using Logistic.ConsoleClient.Classes;
using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Services
{
    internal class WarehouseService : IService<Warehouse>
    {
        public void Create(Warehouse entity)
        {
            InMemeoryRepository<Warehouse, int>.Create(entity);
        }

        public void Delete(int id)
        {
            InMemeoryRepository<Warehouse, int>.Delete(id);
        }

        public List<Warehouse> GetAll()
        {
            return InMemeoryRepository<Warehouse, int>.ReadAll();
        }

        public Warehouse GetById(int id)
        {
            return InMemeoryRepository<Warehouse, int>.ReadById(id);
        }

        public void LoadCargo(Cargo cargo, int id)
        {
            var warehouse = GetById(id);
            warehouse.Add(cargo);
        }

        public Cargo UnloadCargo(Guid cargoId, int id)
        {
            var warehouse = GetById(id);
            var cargo = warehouse.DeleteByGuid(cargoId);
            return cargo;
        }
    }
}
