
using System.Collections.Specialized;

namespace XmlSerializationSample.ServiceRequest
{
    public class RequestOptions
    {
        public string Method { get; set; }
        public string ContentType { get; set; }
        public string Type { get; set; }
        public string Start { get; set; }
        public string Boundary { get; set; }
        public string Action { get; set; }
        public string Uri { get; set; }
        public bool HasAttachment { get; set; }
        public NameValueCollection Headers { get; set; }
    }
}
