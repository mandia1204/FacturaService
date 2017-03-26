using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace XmlSerializationSample.Util
{
    public class Serializer
    {
        public string Serialize<T>(T obj, string defaultNs, XmlSerializerNamespaces ns, bool omitXmlDeclaration = false)
        {
            XmlSerializer serializer;
            if (!string.IsNullOrEmpty(defaultNs))
            {
                serializer = new XmlSerializer(typeof(T), defaultNs);
            }
            else
            {
                serializer = new XmlSerializer(typeof(T));
            }

            var xml = "";

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = omitXmlDeclaration,
                Indent = true
            };

            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw, settings))
                {
                    serializer.Serialize(writer, obj, ns);
                    xml = sw.ToString();
                }
            }

            return xml;
        }
    }
}
