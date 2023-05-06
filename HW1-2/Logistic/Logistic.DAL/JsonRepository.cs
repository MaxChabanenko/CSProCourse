using Logistic.Models;
using Logistic.Core;
using System.Reflection;
using System.Text.Json;

namespace Logistic.DAL
{
    public class JsonRepository<T, Tid> : IReportingRepository<T, Tid> where T : IEntity<Tid>
    {
        public string Create(List<T> entities)
        {
            string fileName = typeof(T).Name + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".json";
            File.WriteAllText(fileName, JsonSerializer.Serialize(entities));
            return fileName;
        }

        public List<T> Read(string path)
        {
            var file = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<T>>(file);
        }
    }
}
