using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Logistic.ConsoleClient.DataAccess
{
    internal class XmlRepository<T> : IRepository<T> where T : EntityBase
    {
        public void Create(List<T> entities)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            
            using (FileStream fs = new FileStream(typeof(T).Name + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".xml", FileMode.OpenOrCreate))
            {
                    xmlSerializer.Serialize(fs, entities);
            }
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
