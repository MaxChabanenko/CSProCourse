using Logistic.ConsoleClient.DataAccess;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.Classes
{
    public class Warehouse : IEntity<int>
    {
        public Warehouse() 
        {
            Id = IdNext++;
            Cargos = new List<Cargo>();
        }
        //авто-інкремент індексу звісно, примітивний, але користувач бачить індекси, тому може легко ними керувати
        static private int IdNext = 1;
        public int Id { get; set; }
        public List<Cargo> Cargos { get; set; }

        public override string ToString()
        {
            return "Warehouse №" + Id;
        }
    }
}
