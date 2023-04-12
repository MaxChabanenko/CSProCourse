
namespace Logistic.Models
{
    public class Warehouse : IEntity<int>
    {
        public Warehouse() 
        {
            Cargos = new List<Cargo>();
        }

        public int Id { get; set; }
        public List<Cargo> Cargos { get; set; }

        public override string ToString()
        {
            return "Warehouse №" + Id;
        }
    }
}
