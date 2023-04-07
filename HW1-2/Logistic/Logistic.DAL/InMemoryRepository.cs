using Logistic.Core;
using Logistic.Models;

namespace Logistic.DAL
{

    public class InMemoryRepository<TEntity> : IRepository<TEntity>
where TEntity : IEntity<int>

    {
        private List<TEntity> _list = new List<TEntity>();
        private int IdCount = 1;

        public TEntity ReadById(int id)
        {
            var entity = _list.FirstOrDefault(x => x.Id.Equals(id));
            return entity.CloneJson();
        }
        public List<TEntity> ReadAll()
        {
            return _list.CloneJson();
        }
        public int Create(TEntity ent)
        {
            var entCopy= ent.CloneJson();
            entCopy.Id = IdCount++;
            _list.Add(entCopy);
            return entCopy.Id;
        }
        public void Delete(int id)
        {
            var element = _list.FirstOrDefault(x => x.Id.Equals(id));
            _list.Remove(element);
        }
        public void Update(int id, TEntity ent)
        {
            Delete(id);
            Create(ent.CloneJson());
        }
    }


}
