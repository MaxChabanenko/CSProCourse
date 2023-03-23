using System.Xml.Serialization;

namespace Logistic.ConsoleClient.DataAccess
{
    
    public interface IEntity<Tid>
    {
        [XmlIgnore]
        public Tid Id { get; set; }
    }
    public interface IReportingRepository<T, Tid> where T : IEntity<Tid>
    {
        string Create(List<T> entities);
        List<T> Read(string filename);
    }
}
