using System;
using System.IO;
using System.Xml.Serialization;

namespace DromundKaasII.Tools
{
    /// <summary>
    /// A Xml serializer.
    /// </summary>
    /// <typeparam name="T">The type to serialize to / from.</typeparam>
    public class XmlManager<T>
    {
        /// <summary>
        /// The type to serialize from.
        /// </summary>
        public Type Type;

        /// <summary>
        /// Deserializes an object from a file.
        /// </summary>
        /// <param name="Path">The path to the file.</param>
        /// <returns>The deserialized object.</returns>
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

        /// <summary>
        /// Serializes an object into a file.
        /// </summary>
        /// <param name="Path">The path to the file.</param>
        /// <param name="obj">The object to serialize.</param>
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
