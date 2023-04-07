

namespace Logistic.Core
{
    public interface IRepository<TEntity>
    {
        TEntity ReadById(int id);

        List<TEntity> ReadAll();

        int Create(TEntity ent);

        void Delete(int id);

        void Update(int id, TEntity ent);
       
    }
}
