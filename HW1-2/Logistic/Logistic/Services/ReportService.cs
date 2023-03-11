using Logistic.ConsoleClient.DataAccess;

namespace Logistic.ConsoleClient.Services
{
    public enum ReportType { xml, json }

    internal class ReportService<T> where T : EntityBase
    {
        JsonRepository<T> JsonRepository = new JsonRepository<T>();
        XmlRepository<T> XmlRepository = new XmlRepository<T>();
        public string CreateReport(/*string fileName,*/ ReportType type)
        {
            if (type == ReportType.json)
            {
                return JsonRepository.Create(InMemeoryRepository<T, int>.ReadAll());
            }
            else
            {
                return XmlRepository.Create(InMemeoryRepository<T, int>.ReadAll());
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
