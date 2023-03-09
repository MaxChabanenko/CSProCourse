using Logistic.ConsoleClient.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //return _list.Find();
            foreach(TEntity entity in _list)
            {
                //switch class Tentity
                if (entity is Cargo && ((Cargo)entity).Id == id)
                    return entity;
            }
            return null;
        }
        public static List<TEntity> ReadAll()
        {
            return _list;
        }
        public static void Delete(Tid id)
        {
            foreach (var entity in _list)
            {
                if ((object)entity.Id == (object)id)
                    _list.Remove(entity);
            }
        }
        public static void Update(Tid id,TEntity ent)
        {
            Delete(id);
            Create(ent);
        }
    }

    
}
