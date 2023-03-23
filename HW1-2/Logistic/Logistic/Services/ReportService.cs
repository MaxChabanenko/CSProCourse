using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Services
{
    public enum ReportType { xml, json }

    internal class ReportService<T, Tid> where T : IEntity<Tid> where Tid : struct, IEquatable<Tid>
    {
        JsonRepository<T, Tid> JsonRepository = new JsonRepository<T, Tid>();
        XmlRepository<T, Tid> XmlRepository = new XmlRepository<T, Tid>();

        private InMemoryRepository<T, Tid> _memoryRepository;
        public ReportService(InMemoryRepository<T, Tid> memoryRepository)
        {
            _memoryRepository = memoryRepository;
        }

        public string CreateReport(ReportType type)
        {
            if (type == ReportType.json)
            {
                return JsonRepository.Create(_memoryRepository.ReadAll());
            }
            else
            {
                return XmlRepository.Create(_memoryRepository.ReadAll());
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
