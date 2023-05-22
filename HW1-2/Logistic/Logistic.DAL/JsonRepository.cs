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
            Assembly asm = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(asm.Location);

            string fileName = typeof(T).Name + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".json";
            File.WriteAllText(Path.Combine(path, fileName), JsonSerializer.Serialize(entities));

            return Path.Combine(path, fileName);
        }


        public List<T> Read(string filePath)
        {
            var file = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(file);
        }
    }
}
