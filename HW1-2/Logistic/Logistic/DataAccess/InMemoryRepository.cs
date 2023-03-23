using Logistic.ConsoleClient.Classes;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.DataAccess
{

    public /*abstract*/ class InMemoryRepository<TEntity, Tid>
where TEntity : IEntity<Tid>
where Tid : struct, IEquatable<Tid>
    {
        private List<TEntity> _list = new List<TEntity>();

        public TEntity ReadById(Tid id)
        {
            var entity = _list.FirstOrDefault(x => x.Id.Equals(id));
            return entity.CloneJson();
        }
        public List<TEntity> ReadAll()
        {
            return _list.CloneJson();
        }
        public void Create(TEntity ent)
        {
            _list.Add(ent);
        }
        public void Delete(Tid id)
        {
            var element = _list.FirstOrDefault(x => x.Id.Equals(id));
            _list.Remove(element);
        }
        public void Update(Tid id, TEntity ent)
        {
            Delete(id);
            Create(ent);
        }
    }


}
