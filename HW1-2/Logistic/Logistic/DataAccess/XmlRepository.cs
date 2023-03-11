using System.Reflection;
using System.Xml.Serialization;

namespace Logistic.ConsoleClient.DataAccess
{
    internal class XmlRepository<T> : IRepository<T> where T : EntityBase
    {
        public string Create(List<T> entities)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            string fileName = typeof(T).Name + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".xml";
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, entities);
            }
            return fileName;
        }

        public List<T> Read(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));

            Assembly asm = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(asm.Location);
            List<T>? entity = new List<T>();

            using (FileStream fs = new FileStream(path + "\\" + filename, FileMode.OpenOrCreate))
            {
                entity = xmlSerializer.Deserialize(fs) as List<T>;
            }

            return entity;
        }
    }
}
