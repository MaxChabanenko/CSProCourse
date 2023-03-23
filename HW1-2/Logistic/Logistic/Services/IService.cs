using Logistic.ConsoleClient.Classes;
using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Services
{
    internal interface IService<T,Tid> where T : IEntity<Tid>
    {
        void Create(T entity);
        T GetById(int id);
        List<T> GetAll();
        void Delete(int id);
        void LoadCargo(Cargo cargo, int id);
        Cargo UnloadCargo(Guid cargoId, int id);
    }
}
