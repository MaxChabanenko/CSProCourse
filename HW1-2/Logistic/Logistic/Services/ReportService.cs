using Logistic.ConsoleClient.DataAccess;
using Logistic.ConsoleClient.Models;

namespace Logistic.ConsoleClient.Services
{
    public enum ReportType { xml, json }

    internal class ReportService<T, Tid> where T : IEntity<Tid> where Tid : struct, IEquatable<Tid>
    {
        JsonRepository<T, Tid> JsonRepository = new JsonRepository<T, Tid>();
        XmlRepository<T, Tid> XmlRepository = new XmlRepository<T, Tid>();

        //якщо ми передаємо список з клієнтської сторони, як ви зазначили у останньому коментарі, то цей код тут непотрібен

        //private InMemoryRepository<T, Tid> _memoryRepository;
        //public ReportService(InMemoryRepository<T, Tid> memoryRepository)
        //{
        //    _memoryRepository = memoryRepository;
        //}

        public string CreateReport(List<T> entities, ReportType type)
        {
            if (type == ReportType.json)
            {
                return JsonRepository.Create(entities);
            }
            else
            {
                return XmlRepository.Create(entities);
            }
        }
        public List<T> LoadReport(string fileName)
        {
            var stringArr = fileName.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (stringArr[^1] == "xml")
            {
                return XmlRepository.Read(fileName);
            }
            else if (stringArr[^1] == "json")
            {
                return JsonRepository.Read(fileName);
            }
            else
            {
                throw new Exception("Error occured while trying to read a file");
            }
        }
    }
}
