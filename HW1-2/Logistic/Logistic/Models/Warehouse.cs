using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Classes
{
    public class Warehouse : EntityBase
    {
        public Warehouse() 
        {
            Id = IdNext++;
            Cargos = new List<Cargo>();
        }
        //авто-інкремент індексу звісно, примітивний, але користувач бачить індекси, тому може легко ними керувати
        static private int IdNext = 1;
        public new int Id { get; set; }
        public List<Cargo> Cargos { get; set; }

        public void Add(Cargo cargo)
        {
            Cargos.Add(cargo);
        }
        public void Delete(Cargo cargo)
        {
            Cargos.Remove(cargo);
        }
        public Cargo DeleteByGuid(Guid id)
        {
            var item = Cargos.Find(x => x.Id == id);
            Delete(item);
            return item;
        }
        public override string ToString()
        {
            return "Warehouse №" + Id;
        }
    }
}
