using Logistic.Models;

namespace Logistic.Core
{
    internal interface IService<T, Tid> where T : IEntity<Tid>
    {
        int Create(T entity);
        T GetById(int id);
        List<T> GetAll();
        void Delete(int id);
        void LoadCargo(Cargo cargo, int id);
        Cargo UnloadCargo(Guid cargoId, int id);
    }
}
