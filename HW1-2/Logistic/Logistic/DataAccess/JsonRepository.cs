using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Logistic.ConsoleClient.DataAccess
{
    internal class JsonRepository<T> : IRepository<T> where T : EntityBase
    {
        public string Create(List<T> entities)
        {
            StringBuilder sb = new StringBuilder();
            string jsonString;
            foreach (var entity in entities)
            {
                jsonString = JsonSerializer.Serialize(entity);
                sb.AppendLine(jsonString);
            }
            string fileName = typeof(T).Name + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".json";
            File.WriteAllText(fileName, sb.ToString());
            return fileName;
        }

        public List<T> Read(string filename)
        {
            List<T> entities = new List<T>();

            Assembly asm = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(asm.Location);

            var file = File.ReadAllText(path + "\\" + filename);

            var stringArr = file.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var jsonLine in stringArr)
            {
                T? entitiy = JsonSerializer.Deserialize<T>(jsonLine);
                entities.Add(entitiy);
            }
            return entities;
        }
    }
}
