﻿using System;
using System.IO;
using System.Xml.Serialization;

namespace DromundKaasII.Tools
{
    public class XmlManager<T>
    {
        public Type Type;

        public T Load(string Path)
        {
            T temp;

            using (TextReader reader = new StreamReader(Path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                temp = (T)xml.Deserialize(reader);
            }

            return temp;
        }

        public void Save(string Path, object obj)
        {
            using (TextWriter writer = new StreamWriter(Path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
