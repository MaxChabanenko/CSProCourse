using Logistic.Models;

namespace Logistic.Core
{
    public enum ReportType { xml, json }

    public class ReportService<T, Tid> where T : IEntity<Tid> where Tid : struct, IEquatable<Tid>
    {
        private readonly IReportingRepository<T, Tid> JsonRepository;
        private readonly IReportingRepository<T, Tid> XmlRepository;

        public ReportService(IReportingRepository<T, Tid> jsonReportingService, IReportingRepository<T, Tid> xmlRepotingRepository)
        {
            XmlRepository = xmlRepotingRepository;
            JsonRepository = jsonReportingService;
        }

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
