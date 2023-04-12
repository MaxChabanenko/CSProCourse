using Logistic.Models;

namespace Logistic.Core
{
    public interface IReportingRepository<T, Tid> where T : IEntity<Tid>
    {
        string Create(List<T> entities);
        List<T> Read(string filename);
    }
}
