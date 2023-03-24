using System.Xml.Serialization;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.DataAccess
{
    public interface IReportingRepository<T, Tid> where T : IEntity<Tid>
    {
        string Create(List<T> entities);
        List<T> Read(string filename);
    }
}
