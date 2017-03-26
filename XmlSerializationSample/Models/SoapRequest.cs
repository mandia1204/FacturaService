using System.Net;

namespace XmlSerializationSample.Models
{
    public class SoapRequest
    {
        public HttpWebRequest Request { get; set; }
        public string SoapStr { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}
