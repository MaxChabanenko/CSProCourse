using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace Logistic.ConsoleClient.DataAccess
{
    internal class JsonRepository<T> : IRepository<T> where T : EntityBase
    {
        public void Create(List<T> entities)
        {
            StringBuilder sb = new StringBuilder();
            string jsonString;
            foreach (var entity in entities)
            {
                jsonString = JsonSerializer.Serialize(entity);
                sb.AppendLine(jsonString);
            }
            File.WriteAllText(typeof(T).Name + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".json", sb.ToString());
        }

        public List<T> Read(string filename)
        {
            List<T> entities = new List<T>();

            Assembly asm = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(asm.Location);

            var file = File.ReadAllText(path +"\\"+ filename);

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
