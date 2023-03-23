using Logistic.ConsoleClient.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Logistic.ConsoleClient.DataAccess
{
    internal class JsonRepository<T, Tid> : IReportingRepository<T, Tid> where T : IEntity<Tid>
    {
        public string Create(List<T> entities)
        {
            string fileName = typeof(T).Name + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".json";
            File.WriteAllText(fileName, JsonSerializer.Serialize(entities));
            return fileName;
        }

        public List<T> Read(string filename)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(asm.Location);

            var file = File.ReadAllText(Path.Combine(path , filename));

            return JsonSerializer.Deserialize<List<T>>(file);

        }
    }
}
