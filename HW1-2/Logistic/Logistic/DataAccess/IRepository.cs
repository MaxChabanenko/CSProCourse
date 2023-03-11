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
        string Create(List<T> entities);
        List<T> Read(string filename);
    }
}
