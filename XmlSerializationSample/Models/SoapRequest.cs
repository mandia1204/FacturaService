using System.Net;
using System.Xml;

namespace XmlSerializationSample.Models
{
    public class SoapRequest
    {
        public HttpWebRequest Request { get; set; }
        //public XmlDocument SoapXml { get; set; }
        public string SoapStr { get; set; }
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        //public string Header { get; set; }
        //public string RequestContentID { get; set; }
    }
}
