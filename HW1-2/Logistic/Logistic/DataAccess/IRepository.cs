using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Logistic.ConsoleClient.DataAccess
{
    [Serializable]
    public abstract class EntityBase
    {
        [XmlIgnore]
        public int Id { get; set; }
    }
    public interface IRepository<T> where T : EntityBase
    {
        void Create(List<T> entities);
        List<T> Read(string filename);
    }
}
