using Logistic.ConsoleClient.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Classes
{
    public class Warehouse : EntityBase
    {
        //public new int Id { get; set; }
        public List<Cargo> Cargos { get; set; }
        public Warehouse() { }
    }
}
