﻿using System.Reflection;
using System.Xml.Serialization;
using Logistic.Models;
using Logistic.Core;

namespace Logistic.DAL
{
    public class XmlRepository<T, Tid> : IReportingRepository<T, Tid> where T : IEntity<Tid>
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

        public List<T> Read(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            List<T>? entity = new List<T>();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                entity = xmlSerializer.Deserialize(fs) as List<T>;
            }

            return entity;
        }
    }
}
