using Logistic.ConsoleClient.Classes;

namespace Logistic.ConsoleClient.DataAccess
{
    public class BaseEntityGeneric<Tid>
    {
        public Tid Id { get; set; }
    }
    internal static class InMemeoryRepository<TEntity, Tid> where TEntity : EntityBase//BaseEntityGeneric<Tid>
    {
        static private List<TEntity> _list = new List<TEntity>();
        public static void Create(TEntity ent)
        {
            _list.Add(ent);
        }
        public static TEntity ReadById(Tid id)
        {
            foreach (TEntity entity in _list)
            {
                //кращою ідеєю було б робити репозитарій для кожного класу, бо цей клас буде "божественим", але така умова
                //це прям дуже костиль, треба випавляти, але не знаю як
                if (entity is Cargo cargo && id is Guid gid && cargo.Id == gid)
                    return entity;
                else if (entity is Invoice invoice && id is Guid gid1 && invoice.Id == gid1)
                    return entity;
                else if (entity is Vehicle vehicle && id is int gid2 && vehicle.Id == gid2)
                    return entity;
                else if (entity is Warehouse wh && id is int gid3 && wh.Id == gid3)
                    return entity;
            }
            throw new Exception("Id not found");
        }
        public static List<TEntity> ReadAll()
        {
            return _list;
        }
        public static void Delete(Tid id)
        {
            var element = ReadById(id);
            _list.Remove(element);
        }
        public static void Update(Tid id, TEntity ent)
        {
            Delete(id);
            Create(ent);
        }
    }


}
